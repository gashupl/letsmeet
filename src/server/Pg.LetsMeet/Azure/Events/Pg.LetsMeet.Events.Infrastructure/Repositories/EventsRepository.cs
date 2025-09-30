using Microsoft.Xrm.Sdk;
using Pg.LetsMeet.Api.Common.Repositories;
using Pg.LetsMeet.Dataverse.Context;
using Pg.LetsMeet.Events.Domain.Data;

namespace Pg.LetsMeet.Events.Infrastructure.Repositories
{
    public class EventsRepository : RepositoryBase<pg_event>, IEventRepository
    {

        public EventsRepository(IOrganizationServiceFactory factory, Guid? userId = null) : base(factory, userId)
        {
        }


        public EventsRepository(IOrganizationService service) : base(service)
        {
        }

        public IList<pg_event> FindByAccountCode(string accountCode)
        {

            using (var context = new DataverseContext(service))
            {
                var query = from e in context.pg_eventSet
                            join a in context.AccountSet on e.pg_partnerId.Id equals a.Id
                            where e.StateCode == pg_event_statecode.Active 
                                && a.StateCode == account_statecode.Active 
                                && a.AccountNumber == accountCode
                            select e; 
                return query.ToList(); 
            }
        }
    }
}
