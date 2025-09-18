using FakeXrmEasy;
using Pg.LetsMeet.Dataverse.Common.Values;
using Pg.LetsMeet.Dataverse.Context;
using Pg.LetsMeet.Dataverse.Plugins.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Pg.LetsMeet.Dataverse.Plugins.Tests.Plugins
{
    public class SetPartnerOnNewEventPluginTests
    {
        [Fact]
        public void IsContextValid_ValidContext_ReturnsTrue()
        {
            var pluginContext = new XrmFakedPluginExecutionContext()
            {
                PrimaryEntityName = pg_event.EntityLogicalName,
                MessageName = MessageName.Create,
                Mode = (int)ProcessingMode.Synchronous, 
                Stage = (int)ProcessingStage.PreOperation
            };

            var plugin = new SetPartnerOnNewEventPlugin();
            var isValid = plugin.IsContextValid(pluginContext);
            Assert.True(isValid);
        }

        [Fact]
        public void IsContextValid_ValidContext_ReturnsFalse()
        {
            var pluginContext = new XrmFakedPluginExecutionContext()
            {
                PrimaryEntityName = pg_event.EntityLogicalName,
                MessageName = MessageName.Update,
                Mode = (int)ProcessingMode.Synchronous,
                Stage = (int)ProcessingStage.PreOperation
            };

            var plugin = new SetPartnerOnNewEventPlugin();
            var isValid = plugin.IsContextValid(pluginContext);
            Assert.False(isValid);
        }
    }
}
