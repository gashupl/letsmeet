using Microsoft.Xrm.Sdk;
using Pg.LetsMeet.Dataverse.Common.Values;
using Pg.LetsMeet.Dataverse.Context;
using Pg.LetsMeet.Dataverse.Domain;
using Pg.LetsMeet.Dataverse.Shared.Injections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pg.LetsMeet.Dataverse.Plugins.Plugins
{
    public class SetPartnerOnNewEventPlugin : PluginBase
    {
        public override IDependencyLoader DependencyLoader { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
            throw new NotImplementedException();
        }
    }
}
