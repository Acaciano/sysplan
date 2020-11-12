using Sysplan.Crosscutting.Common.Data;
using Sysplan.Crosscutting.Domain.Queries;

namespace Sysplan.Domain.Queries
{
    public class PagedQuery<T> : Query<PagedList<T>> where T:class
    {
        public Page page { get; set; }
        public Order Order { get; set; }
        public Restriction Restriction { get; set; }
        public string Search { get; set; }
        public override bool IsValid()
        {
            return true;
        }
    }
}
