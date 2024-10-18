using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketManagement.Domain.Common;

namespace TicketManagement.Persistence.Config.BaseConfig
{
    public abstract class BaseLookupConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseLookupEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.UniqueName).IsUnique();
        }
    }
}
