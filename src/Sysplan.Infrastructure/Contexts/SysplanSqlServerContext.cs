using Microsoft.EntityFrameworkCore;
using Sysplan.Infrastructure.Mappings;

namespace Sysplan.Infrastructure.Contexts
{
    public class SysplanSqlServerContext : DbContext
    {
        public SysplanSqlServerContext(DbContextOptions<SysplanSqlServerContext> opt) : base(opt)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClienteMap());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
