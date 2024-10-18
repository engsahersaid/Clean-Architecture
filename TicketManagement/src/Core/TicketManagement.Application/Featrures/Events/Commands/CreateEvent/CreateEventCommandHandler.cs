using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TicketManagement.Application.Contracts.Infrastructure;
using TicketManagement.Application.Contracts.Persistence;
using TicketManagement.Application.Models;
using TicketManagement.Domain.Entities;

namespace TicketManagement.Application.Features.Events.Commands.CreateEvent
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<CreateEventCommandHandler> _logger;

        public CreateEventCommandHandler(IMapper mapper, IUnitOfWork uow, IEmailService emailService, ILogger<CreateEventCommandHandler> logger)
        {
            _mapper = mapper;
            _uow = uow;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateEventCommandValidator(_uow);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new Exception("Not Valid Event");

            var @event = _mapper.Map<Event>(request);

            _uow.CommandRepository<Event>().Add(@event);
            var res = await _uow.SaveChangesAsync(cancellationToken);
            if (res > 0)
            {
                var email = new Email() { To = "engsahersaid@gmail.com", Body = $"A new event was created: {request}", Subject = "A new event was created" };

                try
                {
                    await _emailService.SendEmail(email);
                }
                catch (Exception ex)
                {
                    //this shouldn't stop the API from doing else so this can be logged
                    _logger.LogError($"Mailing about event {@event.Id} failed due to an error with the mail service: {ex.Message}");
                }

                return @event.Id;
            }
            else
            {
                throw new Exception("");
            }
        }
    }
}
