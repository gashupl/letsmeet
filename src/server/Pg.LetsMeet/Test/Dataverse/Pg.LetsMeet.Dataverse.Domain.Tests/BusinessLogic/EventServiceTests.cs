using Microsoft.Xrm.Sdk;
using Moq;
using Pg.LetsMeet.Dataverse.Context;
using Pg.LetsMeet.Dataverse.Domain.BusinessLogic.Event;
using Pg.LetsMeet.Dataverse.Domain.DataAccess;
using Pg.LetsMeet.Dataverse.Tests.Shared;
using System;
using Xunit;

namespace Pg.LetsMeet.Dataverse.Domain.Tests.BusinessLogic
{
    public class EventServiceTests : DataverseTestBase
    {
        [Fact]
        public void TrySetPartnerOnNewEvent_MissingPortalUser_DoesNotSetPartner()
        {
            var @event = new pg_event()
            {
                Id = Guid.NewGuid(),
                pg_createdbyportaluserid = null
            }; 
            var repositoriesFactory = new Mock<IRepositoriesFactory>();

            var service = new EventService(repositoriesFactory.Object, CreateTracingService());
            service.TrySetPartnerOnNewEvent(@event);

            Assert.Null(@event.pg_partnerId); 

        }

        [Fact]
        public void TrySetPartnerOnNewEvent_MissingPortaUserAccount_DoesNotSetPartner()
        {
            var portaUserRef = new EntityReference(Contact.EntityLogicalName, Guid.NewGuid());
            var @event = new pg_event()
            {
                Id = Guid.NewGuid(),
                pg_createdbyportaluserid = portaUserRef
            };

            var repo = new Mock<IContactRepository>();
            repo.Setup(r => r.GetParentCustomerRef(portaUserRef.Id)).Returns<EntityReference>(null);

            var repositoriesFactory = new Mock<IRepositoriesFactory>();
            repositoriesFactory.Setup(s => s.Get<IContactRepository>(null)).Returns(repo.Object);

            var service = new EventService(repositoriesFactory.Object, CreateTracingService());
            service.TrySetPartnerOnNewEvent(@event);

            Assert.Null(@event.pg_partnerId);
        }

        [Fact]
        public void TrySetPartnerOnNewEvent_PortaUserAccountExists_SetPartner()
        {
            var portaUserRef = new EntityReference(Contact.EntityLogicalName, Guid.NewGuid());
            var expectedAccountRef = new EntityReference(Account.EntityLogicalName, Guid.NewGuid());
            var @event = new pg_event()
            {
                Id = Guid.NewGuid(),
                pg_createdbyportaluserid = portaUserRef
            };

            var repo = new Mock<IContactRepository>();
            repo.Setup(r => r.GetParentCustomerRef(portaUserRef.Id)).Returns(expectedAccountRef);

            var repositoriesFactory = new Mock<IRepositoriesFactory>();
            repositoriesFactory.Setup(s => s.Get<IContactRepository>(null)).Returns(repo.Object);

            var service = new EventService(repositoriesFactory.Object, CreateTracingService());
            service.TrySetPartnerOnNewEvent(@event);

            Assert.NotNull(@event.pg_partnerId);
            Assert.Equal(expectedAccountRef.Id, @event.pg_partnerId.Id);
        }
    }

}
