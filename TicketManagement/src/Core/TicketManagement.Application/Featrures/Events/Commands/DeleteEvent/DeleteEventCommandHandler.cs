using AutoMapper;
using TicketManagement.Application.Contracts.Persistence;
using TicketManagement.Domain.Entities;
using MediatR;

namespace TicketManagement.Application.Features.Events.Commands.DeleteEvent
{
    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public DeleteEventCommandHandler(IMapper mapper,IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public async Task Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var eventToDelete = await _uow.QueryRepository<Event>().GetByIdAsync(request.EventId);

            if (eventToDelete != null)
                throw new Exception("Event not found");

            _uow.CommandRepository<Event>().Delete(eventToDelete);
            await _uow.SaveChangesAsync(cancellationToken);
        }
    }
}
