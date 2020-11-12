using Sysplan.Crosscutting.Domain.Commands;
using Sysplan.Crosscutting.Domain.Events;
using Sysplan.Crosscutting.Domain.Notifications;
using Sysplan.Crosscutting.Domain.Queries;
using MediatR;
using System.Threading.Tasks;

namespace Sysplan.Crosscutting.Domain.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task<TResponse> SendQuery<TResponse>(Query<TResponse> query) where TResponse : class;
        Task RaiseEvent<T>(T @event) where T : Event;
        bool HasNotification();
        INotificationHandler<DomainNotification> GetNotificationHandler();
    }
}