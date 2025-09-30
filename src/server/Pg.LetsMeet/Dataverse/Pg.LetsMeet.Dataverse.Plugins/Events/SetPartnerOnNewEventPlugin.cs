using Microsoft.Xrm.Sdk;
using Pg.LetsMeet.Dataverse.Common.Values;
using Pg.LetsMeet.Dataverse.Context;
using Pg.LetsMeet.Dataverse.Domain;
using Pg.LetsMeet.Dataverse.Domain.BusinessLogic.Event;
using Pg.LetsMeet.Dataverse.Plugins.Core;
using Pg.LetsMeet.Dataverse.Shared.Injections;

namespace Pg.LetsMeet.Dataverse.Plugins.Events
{
    public class SetPartnerOnNewEventPlugin : PluginBase
    {
        public override IDependencyLoader DependencyLoader { get; set; } = new SetPartnerOnNewEventDependencyLoader();

        public override bool IsContextValid(IPluginExecutionContext pluginExecutionContext)
        {
            if (pluginExecutionContext?.Mode == (int)ProcessingMode.Synchronous
                   && pluginExecutionContext?.PrimaryEntityName == pg_event.EntityLogicalName
                   && pluginExecutionContext.MessageName == MessageName.Create
                   && pluginExecutionContext.Stage == (int)ProcessingStage.PreOperation)
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
            if(pluginExecutionContext.InputParameters.Contains("Target") && pluginExecutionContext.InputParameters["Target"] is Entity)
            {
                var target = (Entity)pluginExecutionContext.InputParameters["Target"];
                var @event = target.ToEntity<pg_event>();
                var eventService = servicesFactory.Get<IEventService>();
                eventService.TrySetPartnerOnNewEvent(@event);
            }
            else
            {
                tracingService.Trace("Target entity not found in input parameters or is not of type Entity");
            }
        }
    }
}
