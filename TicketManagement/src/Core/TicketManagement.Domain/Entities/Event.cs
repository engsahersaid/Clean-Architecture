using TicketManagement.Domain.Common;

namespace TicketManagement.Domain.Entities
{
    public class Event : BaseAuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public string? Artist { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = default!;
    }
}