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
    }
}
