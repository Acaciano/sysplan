using Sysplan.Crosscutting.Common.Extensions;
using Sysplan.Crosscutting.Domain.Interfaces.Repositories;
using Sysplan.Crosscutting.Domain.Model;
using Sysplan.Domain.Events;
using Sysplan.Domain.Interfaces.Events;
using Sysplan.Domain.Interfaces.Repositories;
using MediatR;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Sysplan.Domain.EventHandlers
{
    public class ClienteEventHandler :
        INotificationHandler<AddedClienteEvent>,
        INotificationHandler<UpdatedClienteEvent>,
        INotificationHandler<RemovedClienteEvent>
    {
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IClienteMongoRepository _clienteMongoRepository;
        private readonly ClaimsPrincipal _claims;

        public ClienteEventHandler(
            IEventStoreRepository eventStoreRepository,
            IClienteMongoRepository usuarioMongoRepository,
            ClaimsPrincipal claims)
        {
            _eventStoreRepository = eventStoreRepository;
            _clienteMongoRepository = usuarioMongoRepository;
            _claims = claims;
        }

        public async Task Handle(AddedClienteEvent notification, CancellationToken cancellationToken)
        {
            await _clienteMongoRepository.AddAsync(notification.Cliente);
            await ApplyEvent(notification);
        }

        public async Task Handle(UpdatedClienteEvent notification, CancellationToken cancellationToken)
        {
            await _clienteMongoRepository.UpdateAsync(notification.Cliente.Id, notification.Cliente);
            await ApplyEvent(notification);
        }

        public async Task Handle(RemovedClienteEvent notification, CancellationToken cancellationToken)
        {
            await _clienteMongoRepository.RemoveAsync(notification.Cliente.Id);
            await ApplyEvent(notification);
        }

        private async Task ApplyEvent(IClienteEvent notification)
        {
            var eventStore = new EventStore()
            {
                Data = notification.Cliente,
                Id = Guid.NewGuid(),
                StoreType = notification.GetType().Name,
                TimeStamp = DateTime.Now,
                UserId = _claims.GetUserIdFromToken(),
                UserName = _claims.GetUserNameFromToken()
            };

            await _eventStoreRepository.AddAsync(eventStore);
        }
    }
}