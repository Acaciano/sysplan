using Sysplan.Crosscutting.Common.Security;
using Sysplan.Crosscutting.Domain.Bus;
using Sysplan.Crosscutting.Domain.Queries;
using Sysplan.Domain.Interfaces.Repositories;
using Sysplan.Domain.Models;
using Sysplan.Domain.Queries;
using System.Threading.Tasks;

namespace Sysplan.Domain.QueryHandlers
{
    public class GetAuthQueryHandler : MediatorQueryHandler<GetAuthQuery, Cliente>
    {
        private readonly IClienteMongoRepository _clienteMongoRepository;

        public GetAuthQueryHandler(
            IClienteMongoRepository clienteMongoRepository,
            IMediatorHandler mediator) : base(mediator)
        {
            _clienteMongoRepository = clienteMongoRepository;
        }

        public override async Task<Cliente> AfterValidation(GetAuthQuery request)
        {
            var usuarioModel = await _clienteMongoRepository.BuscarPorEmailESenha(request.Email, request.Senha.GetSha1Hash());

            if(usuarioModel == null)
                NotifyError("Validation Query Auth", "Usuário ou Senha: Inválido.");

            return usuarioModel;
        }
    }
}