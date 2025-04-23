using Microsoft.Xrm.Sdk;
using Pg.LetsMeet.Dataverse.Domain;
using Pg.LetsMeet.Dataverse.Domain.BusinessLogic.Environment;
using Pg.LetsMeet.Dataverse.Shared.Injections;

namespace Pg.LetsMeet.Dataverse.Plugins.Plugins.Common
{
    public class ReadEnvironmentVariableSamplePlugin : PluginBase
    {
        public override IDependencyLoader DependencyLoader { get; set; } = new CommonDependencyLoader();

        public override void Execute(IPluginExecutionContext pluginExecutionContext, IServicesFactory servicesFactory, ITracingService tracingService)
        {
            var service = servicesFactory.Get<IVariablesService>();
            var value = service.GetSampleUrl();
            tracingService.Trace(value); 
        }

        public override bool IsContextValid(IPluginExecutionContext pluginExecutionContext)
        {
            return true; 
        }
    }
}
