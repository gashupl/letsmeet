using FakeXrmEasy;
using Pg.LetsMeet.Dataverse.Context;
using Pg.LetsMeet.Dataverse.Plugins.Plugins;
using Xunit;
using Moq;
using System;
using Microsoft.Xrm.Sdk;
using System.Collections.Generic;
using Pg.LetsMeet.Dataverse.Domain;
using Pg.LetsMeet.Dataverse.Domain.BusinessLogic.EventParticipations;
using Pg.LetsMeet.Dataverse.Shared.Values;
using Pg.LetsMeet.Dataverse.Common.Values;
using Pg.LetsMeet.Dataverse.Shared.Injections;

namespace Pg.LetsMeet.Dataverse.Plugins.Tests.Plugins
{

    public class CalculateEventParticipantsPluginTests : DataverseTestBase
    {
        private class CalculateEventParticipantsDependencyLoaderFake : IDependencyLoader
        {
            public IServicesFactory DomainServicesFactory;
            public Mock<IEventParticipationService> EventServiceMock;

            public CalculateEventParticipantsDependencyLoaderFake()
            {
                Mock<IServicesFactory> servicesFactoryMock = new Mock<IServicesFactory>();

                EventServiceMock = new Mock<IEventParticipationService>();
                servicesFactoryMock.Setup(m => m.Get<IEventParticipationService>()).Returns(EventServiceMock.Object);
                
                DomainServicesFactory = servicesFactoryMock.Object; 
            }

            public void SetRegistrations(IContainer container)
            {
                throw new NotImplementedException(); 
            }
        }

        [Fact]
        public void IsContextValid_MissingContext_ReturnsFalse()
        {
            var plugin = new CalculateEventParticipantsPlugin();
            var isValid = plugin.IsContextValid(null);

            Assert.False(isValid);
        }

        [Fact]
        public void IsContextValid_ValidContext_ReturnsTrue()
        {
            var pluginContext = new XrmFakedPluginExecutionContext()
            {
                PrimaryEntityName = pg_eventparticipation.EntityLogicalName,
                MessageName = MessageName.Create,
                Mode = (int)ProcessingMode.Asynchronous
            };

            var plugin = new CalculateEventParticipantsPlugin();
            plugin.IsContextValid(pluginContext); 
        }

        [Fact]
        public void IsContextValid_InvalidContext_ReturnsFalse()
        {
            var pluginContext = new XrmFakedPluginExecutionContext()
            {
                PrimaryEntityName = pg_eventparticipation.EntityLogicalName,
                MessageName = MessageName.Create,
                Mode = (int)ProcessingMode.Synchronous
            };

            var plugin = new CalculateEventParticipantsPlugin();
            plugin.IsContextValid(pluginContext);
        }

        [Fact]
        public void Execute_PostImageExists_CallPostEventsMethods()
        {
            var pluginContext = new XrmFakedPluginExecutionContext()
            {
                PrimaryEntityName = pg_eventparticipation.EntityLogicalName, 
                PostEntityImages = new EntityImageCollection(), 
                Stage = (int)ProcessingStage.PostOperation
            };
            pluginContext.PostEntityImages
                .Add(new KeyValuePair<string, Entity>(ImageName.PostImage, new pg_eventparticipation()
                {
                    pg_eventId = new EntityReference(pg_event.EntityLogicalName, Guid.NewGuid())
                }));

            var loader = new CalculateEventParticipantsDependencyLoaderFake();

            var plugin = new CalculateEventParticipantsPlugin
            {
                DependencyLoader = loader
            };
            plugin.Execute(pluginContext, loader.DomainServicesFactory, CreateTracingService());

            loader.EventServiceMock.Verify(m => m.CountParticipants(It.IsAny<Guid>()), Times.Once);
            loader.EventServiceMock.Verify(m => m.TryUpdateParticipantsNumber(It.IsAny<Guid>(), It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void Execute_PreImageExists_CallPreEventsMethods()
        {
            var pluginContext = new XrmFakedPluginExecutionContext()
            {
                PrimaryEntityName = pg_eventparticipation.EntityLogicalName,
                PreEntityImages = new EntityImageCollection(),
                Stage = (int)ProcessingStage.PreOperation
            };

            pluginContext.PreEntityImages
                .Add(new KeyValuePair<string, Entity>(ImageName.PreImage, new pg_eventparticipation()
                {
                    pg_eventId = new EntityReference(pg_event.EntityLogicalName, Guid.NewGuid())
                }));

            var loader = new CalculateEventParticipantsDependencyLoaderFake();

            var plugin = new CalculateEventParticipantsPlugin
            {
                DependencyLoader = loader
            };
            plugin.Execute(pluginContext, loader.DomainServicesFactory, CreateTracingService());

            loader.EventServiceMock.Verify(m => m.CountParticipants(It.IsAny<Guid>()), Times.Once);
            loader.EventServiceMock.Verify(m => m.TryUpdateParticipantsNumber(It.IsAny<Guid>(), It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void Execute_MissingPostImage_DoNothing()
        {
            var pluginContext = new XrmFakedPluginExecutionContext()
            {
                PrimaryEntityName = pg_eventparticipation.EntityLogicalName,
                PostEntityImages = new EntityImageCollection()
            };

            var loader = new CalculateEventParticipantsDependencyLoaderFake();

            var plugin = new CalculateEventParticipantsPlugin
            {
                DependencyLoader = loader
            };
            plugin.Execute(pluginContext, loader.DomainServicesFactory, CreateTracingService());

            loader.EventServiceMock.Verify(m => m.CountParticipants(It.IsAny<Guid>()), Times.Never);
            loader.EventServiceMock.Verify(m => m.TryUpdateParticipantsNumber(It.IsAny<Guid>(), It.IsAny<int>()), Times.Never);
        }
    }
}
