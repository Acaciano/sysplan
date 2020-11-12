using Sysplan.Domain.Interfaces.UnitOfWork;
using Sysplan.Infrastructure.Contexts;
using System.Threading.Tasks;

namespace Sysplan.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SysplanSqlServerContext _context;

        public UnitOfWork(SysplanSqlServerContext context)
        {
            _context = context;
        }

        public async Task<bool> CommitAsync()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                bool success = (await _context.SaveChangesAsync()) > 0;

                if (success)
                    await transaction.CommitAsync();
                else
                    await transaction.RollbackAsync();

                return success;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
