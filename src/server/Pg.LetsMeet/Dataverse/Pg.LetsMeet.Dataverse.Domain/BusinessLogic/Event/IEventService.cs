using Pg.LetsMeet.Dataverse.Context;

namespace Pg.LetsMeet.Dataverse.Domain.BusinessLogic.Event
{
    public interface IEventService
    {
        void TrySetPartnerOnNewEvent(pg_event @event); 
    }
}
