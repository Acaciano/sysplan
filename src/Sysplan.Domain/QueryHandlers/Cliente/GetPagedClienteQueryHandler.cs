using Sysplan.Crosscutting.Common.Data;
using Sysplan.Crosscutting.Domain.Bus;
using Sysplan.Crosscutting.Domain.Queries;
using Sysplan.Domain.Interfaces.Repositories;
using Sysplan.Domain.Models;
using Sysplan.Domain.Queries;
using System.Threading.Tasks;

namespace Sysplan.Domain.QueryHandlers
{
    public class GetPagedClienteQueryHandler : MediatorQueryHandler<GetPagedClienteQuery, PagedList<Cliente>>
    {
        private readonly IClienteMongoRepository _clienteMongoRepository;

        public GetPagedClienteQueryHandler(
            IClienteMongoRepository clienteMongoRepository,
            IMediatorHandler mediator) : base(mediator)
        {
            _clienteMongoRepository = clienteMongoRepository;
        }

        public override async Task<PagedList<Cliente>> AfterValidation(GetPagedClienteQuery request)
        {
            return await _clienteMongoRepository
                .GetAllPagedAsync(request.Restriction, request.Order, request.page);
        }
    }
}