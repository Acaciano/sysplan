using Sysplan.Crosscutting.Infrastructure.Repositories;
using Sysplan.Domain.Interfaces.Repositories;
using Sysplan.Domain.Models;
using Sysplan.Infrastructure.Contexts;

namespace Sysplan.Infrastructure.Repositories.Demo
{
    public class ClienteSqlServerRepository : SqlServerRepository<Cliente>, IClienteSqlServerRepository
    {
        public ClienteSqlServerRepository(SysplanSqlServerContext context) : base(context: context)
        {
        }
    }
}
