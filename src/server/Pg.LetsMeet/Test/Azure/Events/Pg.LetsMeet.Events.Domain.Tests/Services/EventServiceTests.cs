using Moq;
using Pg.LetsMeet.Dataverse.Context;
using Pg.LetsMeet.Events.Domain.Data;
using Pg.LetsMeet.Events.Domain.Services;
using System;
using Xunit;

namespace Pg.LetsMeet.Events.Domain.Tests.Services
{
    public class EventServiceTests
    {
        [Fact]
        public void CreateEvent_ExistingPartner()
        {
            var accountNumber = "1234";
            var eventRepo = new Mock<IEventRepository>();
            eventRepo.Setup(e => e.Create(It.IsAny<pg_event>()));
            var accountRepo = new Mock<IAccountRepository>();
            accountRepo.Setup(a => a.Update(It.IsAny<Account>()));
            accountRepo.Setup(a => a.FindByAccountCode(accountNumber))
                .Returns(new Account());

            var service = new EventService(eventRepo.Object, accountRepo.Object);

            service.CreateEvent(new Model.EventCreateDto
            {
                PartnerId = accountNumber
            });

            eventRepo.Verify(e => e.Create(It.IsAny<pg_event>()), Times.Once);
            accountRepo.Verify(e => e.Create(It.IsAny<Account>()), Times.Never);
            accountRepo.Verify(e => e.Update(It.IsAny<Account>()), Times.Once);
        }

        [Fact]
        public void CreateEvent_MissingPartnerId()
        {
            var eventRepo = new Mock<IEventRepository>();
            eventRepo.Setup(e => e.Create(It.IsAny<pg_event>())); 
            var accountRepo = new Mock<IAccountRepository>();
            accountRepo.Setup(a => a.Create(It.IsAny<Account>())); 

            var service = new EventService(eventRepo.Object, accountRepo.Object);

            service.CreateEvent(new Model.EventCreateDto());

            eventRepo.Verify(e => e.Create(It.IsAny<pg_event>()), Times.Once);
            accountRepo.Verify(e => e.Create(It.IsAny<Account>()), Times.Once);
        }

        [Fact]
        public void CreateEvent_NewPartner()
        {
            var accountNumber = "1234";
            var eventRepo = new Mock<IEventRepository>();
            eventRepo.Setup(e => e.Create(It.IsAny<pg_event>()));
            var accountRepo = new Mock<IAccountRepository>();
            accountRepo.Setup(a => a.Create(It.IsAny<Account>()));
            accountRepo.Setup(a => a.FindByAccountCode(accountNumber))
                .Returns<Account>(null);

            var service = new EventService(eventRepo.Object, accountRepo.Object);

            Assert.Throws<ArgumentException>(() => service.CreateEvent(new Model.EventCreateDto
            {
                PartnerId = accountNumber
            }));

        }
    }
}
