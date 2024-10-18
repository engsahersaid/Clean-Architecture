using AutoMapper;
using TicketManagement.Application.Contracts.Persistence;
using TicketManagement.Domain.Entities;
using MediatR;

namespace TicketManagement.Application.Features.Events.Queries.GetEventDetail
{
    public class GetEventDetailQueryHandler : IRequestHandler<GetEventDetailQuery, EventDetailVm>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetEventDetailQueryHandler(
            IMapper mapper,
            IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<EventDetailVm> Handle(GetEventDetailQuery request, CancellationToken cancellationToken)
        {
            var @event = await _uow.QueryRepository<Event>().GetAsync(e=>e.Id==request.Id,nameof(Category));
            var eventDetailDto = _mapper.Map<EventDetailVm>(@event);

            return eventDetailDto;
        }
    }
}
