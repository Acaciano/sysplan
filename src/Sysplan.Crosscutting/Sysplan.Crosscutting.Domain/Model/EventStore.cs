using System;

namespace Sysplan.Crosscutting.Domain.Model
{
    public class EventStore : Entity
    {
        public string StoreType { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public DateTime TimeStamp { get; set; }
        public object Data { get; set; }
    }
}