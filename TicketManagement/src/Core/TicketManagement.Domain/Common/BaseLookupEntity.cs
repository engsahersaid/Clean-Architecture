namespace TicketManagement.Domain.Common
{
    public abstract class BaseLookupEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string UniqueName { get; set; }
    }
}