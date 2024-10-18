using TicketManagement.Domain.Common;

namespace TicketManagement.Domain.Entities
{
    public class Category : BaseLookupEntity
    {
        public ICollection<Event>? Events { get; set; }
    }
}