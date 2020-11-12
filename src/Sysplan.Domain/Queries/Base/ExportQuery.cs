using Sysplan.Crosscutting.Common.Data;
using Sysplan.Crosscutting.Domain.Queries;
using System;

namespace Sysplan.Domain.Queries.Base
{
    public class ExportQuery<T> : Query<ExportObject<T>> where T : class
    {
        public Order Order { get; set; }
        public Guid IdAggregate { get; set; }
        public string Kind { get; set; }
        public string Search { get; set; }
        public override bool IsValid()
        {
            return true;
        }
    }
}
