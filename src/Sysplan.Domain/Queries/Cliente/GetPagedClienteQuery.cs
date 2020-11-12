using Sysplan.Crosscutting.Common.Data;
using Sysplan.Domain.Models;

namespace Sysplan.Domain.Queries
{
    public class GetPagedClienteQuery : ClienteQuery<PagedList<Cliente>>
    {
        public override bool IsValid()
        {
            return true;
        }
    }
}