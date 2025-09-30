using Microsoft.Xrm.Sdk;
using Pg.LetsMeet.Dataverse.Context;
using Pg.LetsMeet.Dataverse.Domain.DataAccess;
using System;

namespace Pg.LetsMeet.Dataverse.Domain.BusinessLogic.Event
{
    public class EventService : ServiceBase, IEventService
    {
        private IContactRepository _contactRepository;
        public EventService(IRepositoriesFactory repositoryFactory, ITracingService tracing) : base(repositoryFactory, tracing)
        {
            _contactRepository = repositoryFactory.Get<IContactRepository>();
        }

        public void TrySetPartnerOnNewEvent(pg_event @event)
        {
            var portalUserId = @event.pg_createdbyportaluserid;
            if (portalUserId != null)
            {
                var accountRef = _contactRepository.GetParentCustomerRef(portalUserId.Id); 
                if(accountRef != null)
                {
                    @event.pg_partnerId = accountRef;
                }
                else
                {
                    tracing.Trace($"No parent customer found for contact {portalUserId.Id}, partner not set on event {@event.Id}");
                }
            }
            else
            {
                tracing.Trace($"No portal user found on event {@event.Id}, partner not set");
            }
            
        }
    }
}
