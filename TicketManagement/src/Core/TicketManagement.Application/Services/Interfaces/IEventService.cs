using TicketManagement.Application.ViewModels.Event;

namespace TicketManagement.Application.Services.Interfaces
{
    public interface IEventService
    {
        Task<EventDetailVm> GetEventDetail(int eventId, CancellationToken cancellationToken);
        Task<List<EventListVm>> GetEventsList(CancellationToken cancellationToken);
    }
}