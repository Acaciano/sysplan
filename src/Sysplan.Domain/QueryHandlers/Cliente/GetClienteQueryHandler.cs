using Sysplan.Crosscutting.Domain.Bus;
using Sysplan.Crosscutting.Domain.Queries;
using Sysplan.Domain.Interfaces.Repositories;
using Sysplan.Domain.Models;
using Sysplan.Domain.Queries;
using System.Threading.Tasks;

namespace Sysplan.Domain.QueryHandlers
{
    public class GetClienteQueryHandler : MediatorQueryHandler<GetClienteQuery, Cliente>
    {
        private readonly IClienteMongoRepository _clienteMongoRepository;

        public GetClienteQueryHandler(
            IClienteMongoRepository usuarioMongoRepository,
            IMediatorHandler mediator) : base(mediator)
        {
            _clienteMongoRepository = usuarioMongoRepository;
        }

        public override async Task<Cliente> AfterValidation(GetClienteQuery request)
        {
            return await _clienteMongoRepository.GetOneAsync(x => x.Id == request.Id);
        }
    }
}