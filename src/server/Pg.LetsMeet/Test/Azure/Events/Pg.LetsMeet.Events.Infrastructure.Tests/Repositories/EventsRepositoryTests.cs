using Pg.LetsMeet.Azure.Core;
using Pg.LetsMeet.Dataverse.Context;
using Pg.LetsMeet.Events.Infrastructure.Repositories;
using System;
using Microsoft.Xrm.Sdk; 
using Xunit;

namespace Pg.LetsMeet.Events.Infrastructure.Tests.Repositories
{
    public class EventsRepositoryTests : TestBase
    {
        [Fact]
        public void FindByPartnerId_EventsExists_ReturnList()
        {
            string partnerCode = "AC-123456";
            Guid partnerId = Guid.NewGuid();
            var service = this.FakeOrganizationService;
            service.Create(new Account()
            {
                Id = partnerId, 
                AccountNumber = partnerCode
            });
            service.Create(new pg_event()
            {
                pg_partnerId = new EntityReference(Account.EntityLogicalName, partnerId)
            });
            var serviceFactory = new FakeOrganizationServiceFactory(service);
            var repo = new EventsRepository(serviceFactory);
            var events = repo.FindByAccountCode(partnerCode);
            Assert.NotEqual(0, events.Count); 
        }

        [Fact]
        public void FindByPartnerId_MissingEvents_ReturnEmptyList()
        {
            var service = this.FakeOrganizationService;
            service.Create(new pg_event()
            {
                pg_partnerId = new EntityReference(Account.EntityLogicalName, Guid.NewGuid())
            });
            var serviceFactory = new FakeOrganizationServiceFactory(service);
            var repo = new EventsRepository(serviceFactory);
            var events = repo.FindByAccountCode("AC-123456");
            Assert.Equal(0, events.Count);
        }
    }
}
