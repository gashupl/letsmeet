using System;

namespace Pg.LetsMeet.Dataverse.Domain.BusinessLogic.EventParticipations
{
    public interface IEventParticipationService : IService
    {
        int CountParticipants(Guid eventId);

        void TryUpdateParticipantsNumber(Guid eventId, int number); 
    }
}
