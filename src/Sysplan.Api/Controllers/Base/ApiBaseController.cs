using Sysplan.Crosscutting.Domain.Bus;
using Sysplan.Crosscutting.Domain.Controller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Sysplan.Api.Controllers
{
    [Route("v{version:apiVersion}/[Controller]")]
    [ApiController]
    [Authorize]
    public class ApiBaseController : ApiController
    {
        public ApiBaseController(IMediatorHandler mediator) : base(mediator)
        {
        }
    }
}