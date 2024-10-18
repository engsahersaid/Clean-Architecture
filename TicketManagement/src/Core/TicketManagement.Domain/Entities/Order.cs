using TicketManagement.Domain.Common;

namespace TicketManagement.Domain.Entities
{
    public class Order : BaseAuditableEntity
    {
        public Guid UserId { get; set; }
        public int OrderTotal { get; set; }
        public DateTime OrderPlaced { get; set; }
        public bool OrderPaid { get; set; }
    }
}