using Sysplan.Crosscutting.Domain.Interfaces.Repositories;
using Sysplan.Domain.Models;

namespace Sysplan.Domain.Interfaces.Repositories
{
    public interface IClienteSqlServerRepository : ISqlServerRepository<Cliente>
    {
    }
}