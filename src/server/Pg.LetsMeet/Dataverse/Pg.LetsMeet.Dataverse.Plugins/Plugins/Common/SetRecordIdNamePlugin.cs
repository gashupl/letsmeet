using Microsoft.Xrm.Sdk;
using Pg.LetsMeet.Dataverse.Common.Values;
using Pg.LetsMeet.Dataverse.Domain;
using Pg.LetsMeet.Dataverse.Shared.Injections;
using Pg.LetsMeet.Dataverse.Shared.Values;
using System.Collections.Generic;

namespace Pg.LetsMeet.Dataverse.Plugins.Plugins.Common
{
    public class SetRecordIdNamePlugin : PluginBase
    {
        public override IDependencyLoader DependencyLoader { get; set; } = new CommonDependencyLoader();

        public override void Execute(IPluginExecutionContext pluginExecutionContext, IServicesFactory servicesFactory, ITracingService tracingService)
        {
            if (pluginExecutionContext.InputParameters.ContainsKey(ParameterName.Target))
            {
                var target = (Entity)pluginExecutionContext.InputParameters[ParameterName.Target];

                if (target.Attributes.Contains(CommonAttributesName.EntityIdentifier))
                {
                    target.Attributes[CommonAttributesName.EntityIdentifier] = target.Id.ToString();
                }
                else 
                {
                    target.Attributes.Add(
                        new KeyValuePair<string, object>(CommonAttributesName.EntityIdentifier, target.Id.ToString())); 
                }

                if (target.Attributes.Contains(CommonAttributesName.EntityName))
                {
                    target.Attributes[CommonAttributesName.EntityName] = target.LogicalName; 
                }
                else
                {
                    target.Attributes.Add(
                        new KeyValuePair<string, object>(CommonAttributesName.EntityName, target.LogicalName));
                }
            }
        }

        public override bool IsContextValid(IPluginExecutionContext pluginExecutionContext)
        {
            if ( pluginExecutionContext.Mode == (int)ProcessingMode.Synchronous
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
    }
}
