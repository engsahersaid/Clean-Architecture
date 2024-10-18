using MediatR;
using TicketManagement.Application.Models;

namespace TicketManagement.Application.Features.Orders.Queries.GetOrdersForMonth
{
    public class GetOrdersForMonthQuery : IRequest<PageResult<OrdersForMonthDto>>
    {
        public DateTime Date { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
