using Microsoft.Xrm.Sdk;
using Moq;
using Pg.LetsMeet.Dataverse.Context;
using Pg.LetsMeet.Dataverse.Domain.BusinessLogic.EventParticipations;
using Pg.LetsMeet.Dataverse.Domain.DataAccess;
using System;
using System.Collections.Generic;
using Xunit;

namespace Pg.LetsMeet.Dataverse.Plugins.Tests.BusinessLogic
{
    public class EventParticipationServiceTests : DataverseTestBase
    {
        [Fact]
        public void CountParticipants_ParticipantsExists_ReturnCount()
        {
            int expected = 2; 

            var repo = new Mock<IEventParticipationRepository>();
            repo.Setup(r => r.GetActiveByEventId(It.IsAny<Guid>())).Returns(
                new List<pg_eventparticipation>() { 
                    new pg_eventparticipation() { Id = Guid.NewGuid() }, 
                    new pg_eventparticipation() { Id = Guid.NewGuid() } }
                ); 

            var repositoriesFactory = new Mock<IRepositoriesFactory>(); 
            repositoriesFactory.Setup(s => s.Get<IEventParticipationRepository>(null)).Returns(repo.Object);

            var service = new EventParticipationService(repositoriesFactory.Object, CreateTracingService());
            var actual = service.CountParticipants(Guid.NewGuid()); 

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CountParticipants_ParticipantsMissingInfo_ThrowException()
        {

            var repo = new Mock<IEventParticipationRepository>();
            repo.Setup(r => r.GetActiveByEventId(It.IsAny<Guid>())).Returns<IList<pg_eventparticipation>>(null);

            var repositoriesFactory = new Mock<IRepositoriesFactory>();
            repositoriesFactory.Setup(s => s.Get<IEventParticipationRepository>(null)).Returns(repo.Object);

            var service = new EventParticipationService(repositoriesFactory.Object, CreateTracingService());

            Assert.Throws<InvalidOperationException>(() => service.CountParticipants(Guid.NewGuid()));


        }
        
        [Fact]
        public void TryUpdateParticipantsNumber_AllowedParticipantsNumberExceeded_ThrowsException()
        {
            var repo = new Mock<IRepository>();
            repo.Setup(r => r.GetEntityById<pg_event>(It.IsAny<Guid>())).Returns(new pg_event()
            {
                Id = Guid.NewGuid(),
                pg_allowedparticipantsquantity = 10
            });

            var repositoriesFactory = new Mock<IRepositoriesFactory>();
            repositoriesFactory.Setup(s => s.Get<IRepository>(null)).Returns(repo.Object);
            repositoriesFactory.Setup(s => s.Get<IEventParticipationRepository>(null))
                .Returns<IEventParticipationRepository>(null);

            var service = new EventParticipationService(repositoriesFactory.Object, CreateTracingService());
            
            Assert.Throws<InvalidPluginExecutionException>(
                () => service.TryUpdateParticipantsNumber(Guid.NewGuid(), 11));

        }

        [Fact]
        public void TryUpdateParticipantsNumber_CanAddNewParticipant_CallUpdate()
        {
            var repo = new Mock<IRepository>();
            repo.Setup(r => r.GetEntityById<pg_event>(It.IsAny<Guid>())).Returns(new pg_event()
            {
                Id = Guid.NewGuid(),
                pg_allowedparticipantsquantity = 10
            });
            repo.Setup(r => r.Update(It.IsAny<pg_event>())); 

            var repositoriesFactory = new Mock<IRepositoriesFactory>();
            repositoriesFactory.Setup(s => s.Get<IRepository>(null)).Returns(repo.Object);
            repositoriesFactory.Setup(s => s.Get<IEventParticipationRepository>(null))
                .Returns<IEventParticipationRepository>(null);

            var service = new EventParticipationService(repositoriesFactory.Object, CreateTracingService());

            service.TryUpdateParticipantsNumber(Guid.NewGuid(), 10);

            repo.Verify(r => r.Update(It.IsAny<pg_event>()), Times.Once); 
        }
    }
}
