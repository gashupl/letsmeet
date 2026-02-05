using Microsoft.Xrm.Sdk;
using Moq;
using Pg.LetsMeet.Dataverse.Context;
using Pg.LetsMeet.Dataverse.Domain.BusinessLogic.Contacts;
using Pg.LetsMeet.Dataverse.Domain.BusinessLogic.EventParticipations;
using Pg.LetsMeet.Dataverse.Domain.DataAccess;
using Pg.LetsMeet.Dataverse.Tests.Shared;
using System;
using Xunit;

namespace Pg.LetsMeet.Dataverse.Domain.Tests.BusinessLogic.EventParticipations
{
    public class RegistrationToParticipationServiceTests : DataverseTestBase
    {
        [Fact]
        public void CloseRegistrationForm_SuccessResult_UpdatesStateToAccepted()
        {
            var registrationFormId = Guid.NewGuid();
            var result = CreateParticipationsFromRegistrationsResult.Success;

            var repo = new Mock<IRepository>();
            repo.Setup(r => r.UpdateState(
                registrationFormId,
                pg_eventregistrationform.EntityLogicalName,
                (int)pg_eventregistrationform_statecode.Inactive,
                (int)pg_eventregistrationform_StatusCode.Accepted));

            var contactService = new Mock<IContactService>();

            var repositoriesFactory = new Mock<IRepositoriesFactory>();
            repositoriesFactory.Setup(s => s.Get<IRepository>(null)).Returns(repo.Object);
            repositoriesFactory.Setup(s => s.Get<IContactRepository>(null))
                .Returns(new Mock<IContactRepository>().Object);
            repositoriesFactory.Setup(s => s.Get<IEventParticipationRepository>(null))
                .Returns(new Mock<IEventParticipationRepository>().Object);

            var service = new RegistrationToParticipationService(
                repositoriesFactory.Object, 
                CreateTracingService(), 
                contactService.Object);

            service.CloseRegistrationForm(registrationFormId, result);

            repo.Verify(r => r.UpdateState(
                registrationFormId,
                pg_eventregistrationform.EntityLogicalName,
                (int)pg_eventregistrationform_statecode.Inactive,
                (int)pg_eventregistrationform_StatusCode.Accepted), Times.Once);
        }

        [Fact]
        public void CloseRegistrationForm_FailureResult_UpdatesStateToRejected()
        {
            var registrationFormId = Guid.NewGuid();
            var result = CreateParticipationsFromRegistrationsResult.Failure;

            var repo = new Mock<IRepository>();
            repo.Setup(r => r.UpdateState(
                registrationFormId,
                pg_eventregistrationform.EntityLogicalName,
                (int)pg_eventregistrationform_statecode.Inactive,
                (int)pg_eventregistrationform_StatusCode.Rejected));

            var contactService = new Mock<IContactService>();

            var repositoriesFactory = new Mock<IRepositoriesFactory>();
            repositoriesFactory.Setup(s => s.Get<IRepository>(null)).Returns(repo.Object);
            repositoriesFactory.Setup(s => s.Get<IContactRepository>(null))
                .Returns(new Mock<IContactRepository>().Object);
            repositoriesFactory.Setup(s => s.Get<IEventParticipationRepository>(null))
                .Returns(new Mock<IEventParticipationRepository>().Object);

            var service = new RegistrationToParticipationService(
                repositoriesFactory.Object, 
                CreateTracingService(), 
                contactService.Object);

            service.CloseRegistrationForm(registrationFormId, result);

            repo.Verify(r => r.UpdateState(
                registrationFormId,
                pg_eventregistrationform.EntityLogicalName,
                (int)pg_eventregistrationform_statecode.Inactive,
                (int)pg_eventregistrationform_StatusCode.Rejected), Times.Once);
        }

        [Fact]
        public void TryCreateParticipationsFromRegistrations_ValidRegistrationForm_CreatesParticipationAndReturnsSuccess()
        {
            var contactId = Guid.NewGuid();
            var eventId = new EntityReference(pg_event.EntityLogicalName, Guid.NewGuid());
            var registrationFormId = Guid.NewGuid();
            var registrationForm = new pg_eventregistrationform
            {
                Id = registrationFormId,
                pg_email = "test@example.com",
                pg_firstname = "John",
                pg_lastname = "Doe",
                pg_eventId = eventId
            };

            var contactService = new Mock<IContactService>();
            contactService.Setup(s => s.UpsertContactWithEmail(
                registrationForm.pg_email,
                registrationForm.pg_firstname,
                registrationForm.pg_lastname))
                .Returns(contactId);

            var eventParticipationRepo = new Mock<IEventParticipationRepository>();
            eventParticipationRepo.Setup(r => r.Create(It.IsAny<Entity>())).Returns(Guid.NewGuid());

            var repositoriesFactory = new Mock<IRepositoriesFactory>();
            repositoriesFactory.Setup(s => s.Get<IRepository>(null)).Returns(new Mock<IRepository>().Object);
            repositoriesFactory.Setup(s => s.Get<IContactRepository>(null))
                .Returns(new Mock<IContactRepository>().Object);
            repositoriesFactory.Setup(s => s.Get<IEventParticipationRepository>(null))
                .Returns(eventParticipationRepo.Object);

            var service = new RegistrationToParticipationService(
                repositoriesFactory.Object,
                CreateTracingService(),
                contactService.Object);

            var result = service.TryCreateParticipationsFromRegistrations(registrationForm);

            Assert.Equal(CreateParticipationsFromRegistrationsResult.Success, result);
            eventParticipationRepo.Verify(r => r.Create(It.Is<pg_eventparticipation>(p =>
                p.pg_sourceregistrationformId.Id == registrationFormId &&
                p.pg_contactId.Id == contactId &&
                p.pg_eventId.Id == eventId.Id)), Times.Once);
        }

        [Fact]
        public void TryCreateParticipationsFromRegistrations_CreateThrowsException_ReturnsFailure()
        {
            var contactId = Guid.NewGuid();
            var eventId = new EntityReference(pg_event.EntityLogicalName, Guid.NewGuid());
            var registrationForm = new pg_eventregistrationform
            {
                Id = Guid.NewGuid(),
                pg_email = "test@example.com",
                pg_firstname = "John",
                pg_lastname = "Doe",
                pg_eventId = eventId
            };

            var contactService = new Mock<IContactService>();
            contactService.Setup(s => s.UpsertContactWithEmail(
                registrationForm.pg_email,
                registrationForm.pg_firstname,
                registrationForm.pg_lastname))
                .Returns(contactId);

            var eventParticipationRepo = new Mock<IEventParticipationRepository>();
            eventParticipationRepo.Setup(r => r.Create(It.IsAny<Entity>()))
                .Throws(new InvalidPluginExecutionException("Cannot create participation"));

            var repositoriesFactory = new Mock<IRepositoriesFactory>();
            repositoriesFactory.Setup(s => s.Get<IRepository>(null)).Returns(new Mock<IRepository>().Object);
            repositoriesFactory.Setup(s => s.Get<IContactRepository>(null))
                .Returns(new Mock<IContactRepository>().Object);
            repositoriesFactory.Setup(s => s.Get<IEventParticipationRepository>(null))
                .Returns(eventParticipationRepo.Object);

            var service = new RegistrationToParticipationService(
                repositoriesFactory.Object,
                CreateTracingService(),
                contactService.Object);

            var result = service.TryCreateParticipationsFromRegistrations(registrationForm);

            Assert.Equal(CreateParticipationsFromRegistrationsResult.Failure, result);
            eventParticipationRepo.Verify(r => r.Create(It.IsAny<pg_eventparticipation>()), Times.Once);
        }
    }
}
