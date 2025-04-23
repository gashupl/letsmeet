using Pg.LetsMeet.Events.Domain.Model;

namespace Pg.LetsMeet.Events.Domain.Services
{
    public interface IEventService
    {
        IList<EventDto> FindByAccountCode(string accountCode); 
        void CreateEvent(EventCreateDto @event); 
    }
}
