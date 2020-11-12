using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sysplan.Application.Interfaces.Auth;
using Sysplan.Application.ViewModels;
using Sysplan.Crosscutting.Domain.Bus;
using Sysplan.Crosscutting.Domain.Controller;
using System.Threading.Tasks;

namespace Sysplan.Api.Controllers
{
    [Route("sysplan-api/v1/auth")]
    [ApiController]
    public class AuthController : ApiBaseController
    {
        private readonly IAuthServiceApp _authServiceApp;

        public AuthController(
            IAuthServiceApp authServiceApp,
            IMediatorHandler mediator) : base(mediator)
        {
            _authServiceApp = authServiceApp;
        }

        /// <summary>
        /// Efetua o login
        /// </summary>
        /// <param name="authModel">Dados de entrada para o login</param>
        [AllowAnonymous]
        [HttpPost("token")]
        public async Task<IActionResult> Post([FromBody] AuthViewModel authModel)
        {
            return Response(await _authServiceApp.Auth(authModel));
        }
    }
}