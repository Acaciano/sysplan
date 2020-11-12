using Sysplan.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sysplan.Infrastructure.Mappings
{
    public class ClienteMap : BaseMap<Cliente>
    {
        public override void Configure(EntityTypeBuilder<Cliente> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Nome).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Senha).HasMaxLength(255).IsRequired();

            builder.ToTable("Cliente");
        }
    }
}
