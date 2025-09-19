using Microsoft.Xrm.Sdk;
using Pg.LetsMeet.Dataverse.Context;
using Pg.LetsMeet.Dataverse.Domain.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pg.LetsMeet.Dataverse.Infrastructure
{
    public class EventParticipationRepository : RepositoryBase, IEventParticipationRepository
    {


        public IList<pg_eventparticipation> GetActiveByEventId(Guid eventId)
        {
            using (var context = CreateContext<DataverseContext>())
            {
                var query = context.pg_eventparticipationSet
                    .Where(ep => ep.pg_eventId.Id == eventId && ep.StateCode == pg_eventparticipation_statecode.Active)
                    .Select(ep => ep);

                return query.ToList<pg_eventparticipation>(); 
            }
        }
    }
}
