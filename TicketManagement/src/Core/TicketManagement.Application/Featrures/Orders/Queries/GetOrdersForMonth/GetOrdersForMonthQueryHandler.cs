using AutoMapper;
using MediatR;
using TicketManagement.Application.Contracts.Persistence;
using TicketManagement.Application.Models;
using TicketManagement.Application.Specification;
using TicketManagement.Domain.Entities;

namespace TicketManagement.Application.Features.Orders.Queries.GetOrdersForMonth
{
    public class GetOrdersForMonthQueryHandler : IRequestHandler<GetOrdersForMonthQuery, PageResult<OrdersForMonthDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetOrdersForMonthQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<PageResult<OrdersForMonthDto>> Handle(GetOrdersForMonthQuery request, CancellationToken cancellationToken)
        {
            var speci = new BaseSpecifications<Order>();
            speci = speci.And(new BaseSpecifications<Order>(x => x.OrderPlaced.Month == request.Date.Month && x.OrderPlaced.Year == request.Date.Year));

            var result = _uow.QueryRepository<Order>().FindWithSpecificationPattern(speci, request.PageIndex, request.PageSize);

            return new PageResult<OrdersForMonthDto>(result.PageSize, result.PageIndex, result.TotalCount, _mapper.Map<List<OrdersForMonthDto>>(result.Items));
        }
    }
}
