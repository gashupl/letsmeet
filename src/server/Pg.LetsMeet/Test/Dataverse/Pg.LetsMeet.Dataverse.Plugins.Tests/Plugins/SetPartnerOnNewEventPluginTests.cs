using FakeXrmEasy;
using Microsoft.Xrm.Sdk;
using Moq;
using Pg.LetsMeet.Dataverse.Common.Values;
using Pg.LetsMeet.Dataverse.Context;
using Pg.LetsMeet.Dataverse.Domain;
using Pg.LetsMeet.Dataverse.Domain.BusinessLogic.Event;
using Pg.LetsMeet.Dataverse.Plugins.Plugins;
using Pg.LetsMeet.Dataverse.Shared.Injections;
using Pg.LetsMeet.Dataverse.Shared.Values;
using Pg.LetsMeet.Dataverse.Tests.Shared;
using System;
using System.Collections.Generic;
using Xunit;

namespace Pg.LetsMeet.Dataverse.Plugins.Tests.Plugins
{
    public class SetPartnerOnNewEventPluginTests : DataverseTestBase
    {
        private class SetPartnerOnNewEventDependencyLoaderFake : DependencyLoaderFakeBase, IDependencyLoader
        {
            public Mock<IEventService> EventServiceMock;

            public SetPartnerOnNewEventDependencyLoaderFake()
            {
                Mock<IServicesFactory> servicesFactoryMock = new Mock<IServicesFactory>();

                EventServiceMock = new Mock<IEventService>();
                servicesFactoryMock.Setup(m => m.Get<IEventService>()).Returns(EventServiceMock.Object);

                DomainServicesFactory = servicesFactoryMock.Object;
            }
        }
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

        [Fact]
        public void Execute_ValidTarget_TrySetPartnerOnNewEvent()
        {
            var pluginContext = new XrmFakedPluginExecutionContext()
            {
                PrimaryEntityName = pg_event.EntityLogicalName,
                PostEntityImages = new EntityImageCollection(),
                Stage = (int)ProcessingStage.PreOperation,       
            };

            pluginContext.InputParameters = new ParameterCollection(); 
            pluginContext.InputParameters
                .Add(new KeyValuePair<string, object>(ParameterName.Target, new pg_event()));

            var loader = new SetPartnerOnNewEventDependencyLoaderFake();

            var plugin = new SetPartnerOnNewEventPlugin
            {
                DependencyLoader = loader
            };

            plugin.Execute(pluginContext, loader.DomainServicesFactory, CreateTracingService());

            loader.EventServiceMock.Verify(m => m.TrySetPartnerOnNewEvent(It.IsAny<pg_event>()), Times.Once);
        }

        [Fact]
        public void Execute_InvalidTarget_TraceErrorLog()
        {
            var pluginContext = new XrmFakedPluginExecutionContext()
            {
                PrimaryEntityName = pg_event.EntityLogicalName,
                PostEntityImages = new EntityImageCollection(),
                Stage = (int)ProcessingStage.PreOperation,
            };

            pluginContext.InputParameters = new ParameterCollection();

            var loader = new SetPartnerOnNewEventDependencyLoaderFake();

            var plugin = new SetPartnerOnNewEventPlugin
            {
                DependencyLoader = loader
            };

            plugin.Execute(pluginContext, loader.DomainServicesFactory, CreateTracingService());

            TracingServiceMock.Verify(m => m.Trace
                ("Target entity not found in input parameters or is not of type Entity"), Times.Once);
        }
    }
}
