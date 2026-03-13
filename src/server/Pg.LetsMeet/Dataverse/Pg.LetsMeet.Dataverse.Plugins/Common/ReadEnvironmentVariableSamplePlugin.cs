using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.PluginTelemetry;
using Pg.LetsMeet.Dataverse.Domain;
using Pg.LetsMeet.Dataverse.Domain.BusinessLogic.Environment;
using Pg.LetsMeet.Dataverse.Plugins.Core;
using Pg.LetsMeet.Dataverse.Shared.Injections;
using Pg.LetsMeet.Dataverse.Shared.Services;

namespace Pg.LetsMeet.Dataverse.Plugins.Common
{
    public class ReadEnvironmentVariableSamplePlugin : PluginBase
    {
        public override IDependencyLoader DependencyLoader { get; set; } = new CommonDependencyLoader();

        public override void Execute(IPluginExecutionContext pluginExecutionContext, IServicesFactory servicesFactory, IPluginTracingService tracingService)
        {
            var service = servicesFactory.Get<IVariablesService>();
            var value = service.GetSampleUrl();
            tracingService.Trace(LogLevel.Trace, value); 
        }

        public override bool IsContextValid(IPluginExecutionContext pluginExecutionContext)
        {
            return true; 
        }
    }
}
