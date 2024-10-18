using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketManagement.Domain.Entities;
using TicketManagement.Persistence.Config.BaseConfig;

namespace TicketManagement.Persistence.Config
{
    public class CategoryConfiguration : BaseLookupConfiguration<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");
            base.Configure(builder);
        }
    }
}
