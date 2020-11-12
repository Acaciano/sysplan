using AutoMapper;
using Sysplan.Application.Interfaces;
using Sysplan.Application.Interfaces.Auth;
using Sysplan.Application.ViewModels;
using Sysplan.Application.ViewModels.Token;
using Sysplan.Crosscutting.Domain.Bus;
using Sysplan.Domain.Queries;
using System.Threading.Tasks;

namespace Sysplan.Application.Services.Token
{
    public class AuthServiceApp : IAuthServiceApp
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;
        private readonly ITokenServiceApp _tokenServiceApp;

        public AuthServiceApp(
            IMapper mapper,
            IMediatorHandler mediator,
            ITokenServiceApp tokenServiceApp)
        {
            _mapper = mapper;
            _mediator = mediator;
            _tokenServiceApp = tokenServiceApp;
        }

        public async Task<TokenViewModel> Auth(AuthViewModel authViewModel)
        {
            var query = new GetAuthQuery(authViewModel.Login, authViewModel.Senha);
            var usuarioModel = await _mediator.SendQuery(query);

            if (usuarioModel != null)
            {
                var usuarioViewModel = _mapper.Map<ClienteViewModel>(usuarioModel);
                return _tokenServiceApp.GenereteToken(usuarioViewModel);
            }

            return new TokenViewModel();
        }
    }
}
