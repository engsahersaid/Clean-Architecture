using AutoMapper;
using MediatR;
using TicketManagement.Application.Contracts.Persistence;
using TicketManagement.Domain.Entities;

namespace TicketManagement.Application.Features.Events.Commands.UpdateEvent
{
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public UpdateEventCommandHandler(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public async Task Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {

            var eventToUpdate = await _uow.QueryRepository<Event>().GetByIdAsync(request.EventId);

            if (eventToUpdate == null)
                throw new Exception("Event not found");

            var validator = new UpdateEventCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new Exception("Not Valid Event");

            _mapper.Map(request, eventToUpdate, typeof(UpdateEventCommand), typeof(Event));

            _uow.CommandRepository<Event>().Update(eventToUpdate);
            await _uow.SaveChangesAsync(cancellationToken);
        }
    }
}