using Microsoft.Xrm.Sdk;
using Pg.LetsMeet.Dataverse.Context;
using Pg.LetsMeet.Dataverse.Domain.DataAccess;
using System;

namespace Pg.LetsMeet.Dataverse.Domain.BusinessLogic.EventParticipations
{
    public class EventParticipationService : ServiceBase, IEventParticipationService
    {
        private const string NullParticipantsMessage = "Cannot calculate participants. Missing participants information.";
        private const string CannotAddMoreParticipants = "Cannot add more participants to the selected event. Max participant count is {0}";
        private readonly IEventParticipationRepository _eventParticipationsRepository;
        private readonly IRepository _entityRepository; 

        public EventParticipationService(IRepositoriesFactory repositoryFactory, ITracingService tracing) : base(repositoryFactory, tracing)
        {
            _eventParticipationsRepository = repositoryFactory.Get<IEventParticipationRepository>();
            _entityRepository = repositoryFactory.Get<IRepository>(); 
        }

        public int CountParticipants(Guid eventId)
        {
            var events = _eventParticipationsRepository.GetActiveByEventId(eventId); 

            if(events == null)
            {
                throw new InvalidOperationException(NullParticipantsMessage); 
            }

            return _eventParticipationsRepository.GetActiveByEventId(eventId).Count; 
        }

        public void TryUpdateParticipantsNumber(Guid eventId, int number)
        {
            var @event = _entityRepository.GetEntityById<pg_event>(eventId); 
            if (@event?.pg_allowedparticipantsquantity != null 
                && number > @event.pg_allowedparticipantsquantity)
            {
                throw new InvalidPluginExecutionException(
                    String.Format(CannotAddMoreParticipants, @event?.pg_allowedparticipantsquantity.ToString())); 
            }
            _entityRepository.Update(new pg_event()
            {
                Id = eventId,
                pg_registeredparticipantsquantity = number
            }); 
        }
    }
}
