using Sysplan.Crosscutting.Domain.Events;
using Sysplan.Domain.Interfaces.Events;
using Sysplan.Domain.Models;

namespace Sysplan.Domain.Events
{
    public class AddedClienteEvent : Event, IClienteEvent
    {
        public AddedClienteEvent(Cliente cliente)
        {
            Cliente = cliente;
        }

        public Cliente Cliente { get; set; }
    }
}