using TicketManagement.Application.Features.Orders.Queries.GetOrdersForMonth;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.Application.Models;

namespace TicketManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("/getpagedordersformonth", Name = "GetPagedOrdersForMonth")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<PageResult<OrdersForMonthDto>>> GetPagedOrdersForMonth(DateTime date, int pageIndex, int pageSize)
        {
            var getOrdersForMonthQuery = new GetOrdersForMonthQuery() { Date = date, PageIndex = pageIndex, PageSize = pageSize };
            var dtos = await _mediator.Send(getOrdersForMonthQuery);

            return Ok(dtos);
        }
    }
}
