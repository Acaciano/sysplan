using Sysplan.Crosscutting.Common.Data;
using Sysplan.Crosscutting.Domain.Queries;
using System;

namespace Sysplan.Domain.Queries
{
    public abstract class ClienteQuery<TResponse> : Query<TResponse>
    {
        public Guid Id { get; set; }
        public Page page { get; set; }
        public Order Order { get; set; }
        public Restriction Restriction { get; set; }
    }
}