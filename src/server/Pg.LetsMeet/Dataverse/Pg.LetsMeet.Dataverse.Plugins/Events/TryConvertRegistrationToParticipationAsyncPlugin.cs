using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.PluginTelemetry;
using Pg.LetsMeet.Dataverse.Common.Values;
using Pg.LetsMeet.Dataverse.Context;
using Pg.LetsMeet.Dataverse.Domain;
using Pg.LetsMeet.Dataverse.Domain.BusinessLogic.EventParticipations;
using Pg.LetsMeet.Dataverse.Plugins.Core;
using Pg.LetsMeet.Dataverse.Shared.Injections;
using Pg.LetsMeet.Dataverse.Shared.Services;

namespace Pg.LetsMeet.Dataverse.Plugins.Events
{
    public class TryConvertRegistrationToParticipationAsyncPlugin : PluginBase
    {
        public override IDependencyLoader DependencyLoader { get; set; } 
            = new TryConvertRegistrationToParticipationDependencyLoader();

        public override bool IsContextValid(IPluginExecutionContext pluginExecutionContext)
        {
            if (pluginExecutionContext?.Mode == (int)ProcessingMode.Asynchronous
                   && pluginExecutionContext?.PrimaryEntityName == pg_eventregistrationform.EntityLogicalName
                   && pluginExecutionContext.MessageName == MessageName.Create
                   && pluginExecutionContext.Stage == (int)ProcessingStage.PostOperation)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void Execute(IPluginExecutionContext pluginExecutionContext, IServicesFactory servicesFactory, IPluginTracingService tracingService)
        {
            if (pluginExecutionContext.InputParameters.Contains("Target") && pluginExecutionContext.InputParameters["Target"] is Entity)
            {
                var target = (Entity)pluginExecutionContext.InputParameters["Target"];
                var registrationForm = target.ToEntity<pg_eventregistrationform>();
                var registrationService = servicesFactory.Get<IRegistrationToParticipationService>();
                var result = registrationService.TryCreateParticipationsFromRegistrations(registrationForm);
                registrationService.CloseRegistrationForm(registrationForm.Id, result);
            }
            else
            {
                tracingService.Trace(LogLevel.Trace, "Target entity not found in input parameters or is not of type Entity");
            }
        }


    }
}
