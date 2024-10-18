using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketManagement.Domain.Entities;
using TicketManagement.Persistence.Config.BaseConfig;

namespace TicketManagement.Persistence.Config
{
    public class EventConfiguration : BaseEntityConfiguration<Event>
    {
        public override void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Event");
            builder.Property(e => e.Name).IsRequired().HasMaxLength(50);
            base.Configure(builder);
        }
    }
}
