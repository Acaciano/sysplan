using Sysplan.Domain.Models;

namespace Sysplan.Domain.Interfaces.Events
{
    public interface IClienteEvent
    {
        Cliente Cliente { get; set; }
    }
}