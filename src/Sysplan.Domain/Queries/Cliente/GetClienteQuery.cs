using Sysplan.Domain.Models;

namespace Sysplan.Domain.Queries
{
    public class GetClienteQuery : ClienteQuery<Cliente>
    {
        public override bool IsValid()
        {
            return true;
        }
    }
}