using Microsoft.Xrm.Sdk;
using Pg.LetsMeet.Dataverse.Common.Values;
using Pg.LetsMeet.Dataverse.Context;
using Pg.LetsMeet.Dataverse.Domain;
using Pg.LetsMeet.Dataverse.Domain.BusinessLogic.EventParticipations;
using Pg.LetsMeet.Dataverse.Plugins.Core;
using Pg.LetsMeet.Dataverse.Shared.Injections;
using Pg.LetsMeet.Dataverse.Shared.Values;

namespace Pg.LetsMeet.Dataverse.Plugins.Events
{
    public class CalculateEventParticipantsPlugin : PluginBase
    {
        public override IDependencyLoader DependencyLoader { get; set; } = new CalculateEventParticipantsDependencyLoader();

        public override bool IsContextValid(IPluginExecutionContext pluginExecutionContext)
        {
            if (pluginExecutionContext?.Mode == (int)ProcessingMode.Synchronous
                   && pluginExecutionContext?.PrimaryEntityName == pg_eventparticipation.EntityLogicalName)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void Execute(IPluginExecutionContext pluginExecutionContext, IServicesFactory servicesFactory, ITracingService tracingService)
        {
            if (pluginExecutionContext.Stage == (int)ProcessingStage.PostOperation) //For Calculating during Participant creation and update
            {
                HandlePostEvents(pluginExecutionContext, servicesFactory, tracingService);
            }
            else if (pluginExecutionContext.Stage == (int)ProcessingStage.PreOperation) //For Calculating during Participant deletion
            {
                HandlePreEvents(pluginExecutionContext, servicesFactory, tracingService);
            }

        }

        private void HandlePreEvents(IPluginExecutionContext pluginExecutionContext, IServicesFactory servicesFactory, ITracingService tracingService)
        {
            var preImage = GetPreImageEntity<pg_eventparticipation>(pluginExecutionContext, ImageName.PreImage);
            if (preImage?.pg_eventId != null)
            {
                var eventId = preImage.pg_eventId.Id;
                var service = servicesFactory.Get<IEventParticipationService>();
                var participantsCount = service.CountParticipants(eventId);
                service.TryUpdateParticipantsNumber(eventId, participantsCount - 1);
            }
        }

        private void HandlePostEvents(IPluginExecutionContext pluginExecutionContext, IServicesFactory servicesFactory, ITracingService tracingService)
        {
            var postImage = GetPostImageEntity<pg_eventparticipation>(pluginExecutionContext, ImageName.PostImage);
            if (postImage?.pg_eventId != null)
            {
                var eventId = postImage.pg_eventId.Id;
                var service = servicesFactory.Get<IEventParticipationService>();
                var participantsCount = service.CountParticipants(eventId);
                service.TryUpdateParticipantsNumber(eventId, participantsCount);
            }
        }

    }
}
