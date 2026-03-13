using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.PluginTelemetry;
using Pg.LetsMeet.Dataverse.Context;
using Pg.LetsMeet.Dataverse.Domain.DataAccess;
using Pg.LetsMeet.Dataverse.Shared.Services;
using System;

namespace Pg.LetsMeet.Dataverse.Domain.BusinessLogic.Events
{
    public class EventService : ServiceBase, IEventService
    {
        private IContactRepository _contactRepository;
        public EventService(IRepositoriesFactory repositoryFactory, IPluginTracingService tracing) : base(repositoryFactory, tracing)
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
                    tracing.Trace(LogLevel.Trace, 
                        "No parent customer found for contact {portalUserId}, partner not set on event {eventId}", portalUserId.Id, @event.Id);
                }
            }
            else
            {
                tracing.Trace(LogLevel.Trace, "No portal user found on event {eventId}, partner not set", @event.Id);
            }
            
        }
    }
}
