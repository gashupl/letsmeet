using FakeXrmEasy;
using Microsoft.Xrm.Sdk;
using Moq;
using Pg.LetsMeet.Dataverse.Common.Values;
using Pg.LetsMeet.Dataverse.Context;
using Pg.LetsMeet.Dataverse.Domain;
using Pg.LetsMeet.Dataverse.Domain.BusinessLogic.EventParticipations;
using Pg.LetsMeet.Dataverse.Plugins.Events;
using Pg.LetsMeet.Dataverse.Shared.Injections;
using Pg.LetsMeet.Dataverse.Shared.Values;
using Pg.LetsMeet.Dataverse.Tests.Shared;
using System;
using System.Collections.Generic;
using Xunit;

namespace Pg.LetsMeet.Dataverse.Plugins.Tests.Events
{
    public class TryConvertRegistrationToParticipationAsyncPluginTests : DataverseTestBase
    {
        private class TryConvertRegistrationToParticipationDependencyLoaderFake : DependencyLoaderFakeBase, IDependencyLoader
        {
            public Mock<IRegistrationToParticipationService> RegistrationServiceMock;

            public TryConvertRegistrationToParticipationDependencyLoaderFake()
            {
                Mock<IServicesFactory> servicesFactoryMock = new Mock<IServicesFactory>();

                RegistrationServiceMock = new Mock<IRegistrationToParticipationService>();
                servicesFactoryMock.Setup(m => m.Get<IRegistrationToParticipationService>())
                    .Returns(RegistrationServiceMock.Object);

                DomainServicesFactory = servicesFactoryMock.Object;
            }
        }

        [Fact]
        public void IsContextValid_ValidAsyncContext_ReturnsTrue()
        {
            var pluginContext = new XrmFakedPluginExecutionContext()
            {
                PrimaryEntityName = pg_eventregistrationform.EntityLogicalName,
                MessageName = MessageName.Create,
                Mode = (int)ProcessingMode.Asynchronous,
                Stage = (int)ProcessingStage.PostOperation
            };

            var plugin = new TryConvertRegistrationToParticipationAsyncPlugin();
            var isValid = plugin.IsContextValid(pluginContext);

            Assert.True(isValid);
        }

        [Fact]
        public void IsContextValid_InvalidContext_ReturnsFalse()
        {
            var pluginContext = new XrmFakedPluginExecutionContext()
            {
                PrimaryEntityName = pg_eventregistrationform.EntityLogicalName,
                MessageName = MessageName.Update,
                Mode = (int)ProcessingMode.Asynchronous,
                Stage = (int)ProcessingStage.PostOperation
            };

            var plugin = new TryConvertRegistrationToParticipationAsyncPlugin();
            var isValid = plugin.IsContextValid(pluginContext);

            Assert.False(isValid);
        }

        [Fact]
        public void Execute_ValidTarget_CallsRegistrationServiceMethods()
        {
            var registrationFormId = Guid.NewGuid();
            var registrationForm = new pg_eventregistrationform
            {
                Id = registrationFormId,
                pg_email = "test@example.com",
                pg_firstname = "John",
                pg_lastname = "Doe"
            };

            var pluginContext = new XrmFakedPluginExecutionContext()
            {
                PrimaryEntityName = pg_eventregistrationform.EntityLogicalName,
                Stage = (int)ProcessingStage.PostOperation
            };

            pluginContext.InputParameters = new ParameterCollection();
            pluginContext.InputParameters.Add(new KeyValuePair<string, object>("Target", registrationForm));

            var loader = new TryConvertRegistrationToParticipationDependencyLoaderFake();
            loader.RegistrationServiceMock
                .Setup(m => m.TryCreateParticipationsFromRegistrations(It.IsAny<pg_eventregistrationform>()))
                .Returns(CreateParticipationsFromRegistrationsResult.Success);

            var plugin = new TryConvertRegistrationToParticipationAsyncPlugin
            {
                DependencyLoader = loader
            };

            plugin.Execute(pluginContext, loader.DomainServicesFactory, CreateTracingService());

            loader.RegistrationServiceMock.Verify(
                m => m.TryCreateParticipationsFromRegistrations(It.Is<pg_eventregistrationform>(f => f.Id == registrationFormId)),
                Times.Once);
            loader.RegistrationServiceMock.Verify(
                m => m.CloseRegistrationForm(registrationFormId, CreateParticipationsFromRegistrationsResult.Success),
                Times.Once);
        }

        [Fact]
        public void Execute_InvalidTarget_TracesErrorLog()
        {
            var pluginContext = new XrmFakedPluginExecutionContext()
            {
                PrimaryEntityName = pg_eventregistrationform.EntityLogicalName,
                Stage = (int)ProcessingStage.PostOperation
            };

            pluginContext.InputParameters = new ParameterCollection();

            var loader = new TryConvertRegistrationToParticipationDependencyLoaderFake();

            var plugin = new TryConvertRegistrationToParticipationAsyncPlugin
            {
                DependencyLoader = loader
            };

            plugin.Execute(pluginContext, loader.DomainServicesFactory, CreateTracingService());

            TracingServiceMock.Verify(
                m => m.Trace("Target entity not found in input parameters or is not of type Entity"),
                Times.Once);
            loader.RegistrationServiceMock.Verify(
                m => m.TryCreateParticipationsFromRegistrations(It.IsAny<pg_eventregistrationform>()),
                Times.Never);
            loader.RegistrationServiceMock.Verify(
                m => m.CloseRegistrationForm(It.IsAny<Guid>(), It.IsAny<CreateParticipationsFromRegistrationsResult>()),
                Times.Never);
        }
    }
}
