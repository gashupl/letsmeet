using Pg.LetsMeet.Dataverse.Context;

namespace Pg.LetsMeet.Dataverse.Domain.BusinessLogic.Events
{
    public interface IEventService
    {
        void TrySetPartnerOnNewEvent(pg_event @event); 
    }
}
