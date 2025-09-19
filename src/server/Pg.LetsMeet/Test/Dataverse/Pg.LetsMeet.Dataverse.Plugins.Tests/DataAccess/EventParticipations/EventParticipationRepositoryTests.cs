using System; 
using FakeXrmEasy;
using Microsoft.Xrm.Sdk;
using Pg.LetsMeet.Dataverse.Context;
using System.Collections.Generic;
using System.Reflection;
using Xunit;
using Pg.LetsMeet.Dataverse.Infrastructure;

namespace Pg.LetsMeet.Dataverse.Plugins.Tests.DataAccess.EventParticipations
{
    
    public class EventParticipationRepositoryTests
    {
        [Fact]
        public void GetActiveByEventId_ReturnValidCount()
        {
            //TODO: To be fixed
            var eventRef = new EntityReference(pg_event.EntityLogicalName, Guid.NewGuid());
            var expectedCount = 1;

            var context = new XrmFakedContext();
            context.ProxyTypesAssembly = Assembly.GetAssembly(typeof(pg_eventparticipation));

            context.Initialize(new List<Entity>() {
                new pg_eventparticipation(){ Id = Guid.NewGuid(),
                    pg_eventId = eventRef,
                    StateCode = pg_eventparticipation_statecode.Active},
                new pg_eventparticipation(){ Id = Guid.NewGuid(),
                    pg_eventId = eventRef,
                    StateCode = pg_eventparticipation_statecode.Inactive},
                new pg_eventparticipation(){ Id = Guid.NewGuid(),
                    pg_eventId = new EntityReference(pg_event.EntityLogicalName, Guid.NewGuid()),
                    StateCode = pg_eventparticipation_statecode.Active},
            });

            var service = context.GetOrganizationService();


            var repo = new EventParticipationRepository(); 
            repo.Initialize(service); 
            var actualCount = repo.GetActiveByEventId(eventRef.Id)?.Count;

            Assert.Equal(expectedCount, actualCount);
        }
        
    }
}
