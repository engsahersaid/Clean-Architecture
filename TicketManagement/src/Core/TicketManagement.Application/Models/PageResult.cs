namespace TicketManagement.Application.Models
{
    public class PageResult<T>
    {
        public int PageSize { get; }
        public int PageIndex { get; }
        public long TotalCount { get; }
        public int TotalPages { get; }
        public List<T> Items { get; }

        public PageResult(int pageSize, int pageIndex)
        {
            if (pageSize <= 0)
                PageSize = 10;
            else PageSize = pageSize;

            if (pageIndex <= 0)
                PageIndex = 1;
            else PageIndex = pageIndex;

            TotalCount = 0;
            TotalPages = 0;

            Items = new List<T>();
        }

        public PageResult(int pageSize, int pageIndex, long totalCount, List<T> items) : this(pageSize, pageIndex)
        {
            TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling((double)TotalCount / PageSize);
            Items = items;
        }

        public PageResult(PageResult<T> pageResult, List<T> items, long totalCount) : this(pageResult.PageSize, pageResult.PageIndex, totalCount, items)
        {
        }

        public PageResult(PageResult<T> pageResult, List<T> items) : this(pageResult.PageSize, pageResult.PageIndex, pageResult.TotalCount, items)
        {
        }
    }
}