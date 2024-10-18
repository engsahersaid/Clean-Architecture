using FluentValidation;
using TicketManagement.Application.Contracts.Persistence;
using TicketManagement.Domain.Entities;

namespace TicketManagement.Application.Features.Events.Commands.CreateEvent
{
    public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
    {
        private readonly IUnitOfWork _uow;
        public CreateEventCommandValidator(IUnitOfWork uow)
        {
            _uow = uow;

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.Date)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .GreaterThan(DateTime.Now);

            RuleFor(e => e)
                .MustAsync(EventName)
                .WithMessage("An event with the same name already exists.");

            RuleFor(p => p.Price)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0);
        }

        private async Task<bool> EventName(CreateEventCommand e, CancellationToken token)
        {
            var events = await _uow.QueryRepository<Event>().GetAsync(ev => ev.Name.ToLower() == e.Name.ToLower());
            return events != null;
        }
    }
}
