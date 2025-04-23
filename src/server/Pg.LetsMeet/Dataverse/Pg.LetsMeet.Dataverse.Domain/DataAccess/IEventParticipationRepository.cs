using Pg.LetsMeet.Dataverse.Context;
using System;
using System.Collections.Generic;

namespace Pg.LetsMeet.Dataverse.Domain.DataAccess
{
    public interface IEventParticipationRepository : IRepository
    {
        IList<pg_eventparticipation> GetActiveByEventId(Guid eventId); 
    }
}
