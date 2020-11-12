using MediatR;
using Newtonsoft.Json;

namespace Sysplan.Crosscutting.Domain.Events
{
    public abstract class CommandMessage : IRequest, IRequestBase
    {
        [JsonIgnore()]
        public string MessageType { get; protected set; }

        protected CommandMessage()
        {
            MessageType = GetType().Name;
        }
    }
}