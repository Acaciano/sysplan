using AutoMapper;
using Sysplan.Application.Interfaces;
using Sysplan.Application.ViewModels;
using Sysplan.Crosscutting.Common.Data;
using Sysplan.Crosscutting.Domain.Bus;
using Sysplan.Domain.Commands;
using Sysplan.Domain.Queries;
using System;
using System.Threading.Tasks;

namespace Sysplan.Application.Services
{
    public class ClienteServiceApp : IClienteServiceApp
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;

        public ClienteServiceApp(
            IMapper mapper,
            IMediatorHandler mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<PagedList<ClienteViewModel>> GetPaged(int page, int size,
            string orderProperty, bool orderCrescent, string filterProperty, string filterValue)
        {
            var query = new GetPagedClienteQuery
            {
                page = new Page(page, size),
                Order = new Order(orderProperty, orderCrescent),
                Restriction = new Restriction(filterProperty, Condition.Default, filterValue)
            };

            var demos = await _mediator.SendQuery(query);

            return _mapper.Map<PagedList<ClienteViewModel>>(demos);
        }

        public async Task<ClienteViewModel> Get(Guid id)
        {
            var query = new GetClienteQuery { Id = id };
            var demo = await _mediator.SendQuery(query);

            return _mapper.Map<ClienteViewModel>(demo);
        }

        public async Task<bool> Add(AddClienteCommand command)
        {
            await _mediator.SendCommand(command);
            return !_mediator.HasNotification();
        }

        public async Task<bool> Update(UpdateClienteCommand command)
        {
            await _mediator.SendCommand(command);
            return !_mediator.HasNotification();
        }

        public async Task Remove(Guid id)
        {
            var command = new RemoveClienteCommand(id);
            await _mediator.SendCommand(command);
        }
    }
}