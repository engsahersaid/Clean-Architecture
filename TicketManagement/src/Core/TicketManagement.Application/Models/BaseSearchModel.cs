namespace TicketManagement.Application.Models
{
    public class BaseSearchModel<T> where T : class
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public T? SearchCriteria { get; set; }
    }
}