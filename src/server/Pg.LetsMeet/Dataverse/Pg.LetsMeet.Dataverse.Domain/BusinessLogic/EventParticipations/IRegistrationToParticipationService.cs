using Pg.LetsMeet.Dataverse.Context;
using System;

namespace Pg.LetsMeet.Dataverse.Domain.BusinessLogic.EventParticipations
{
    public interface IRegistrationToParticipationService : IService
    {
        CreateParticipationsFromRegistrationsResult TryCreateParticipationsFromRegistrations(pg_eventregistrationform registrationForm);

        void CloseRegistrationForm(Guid registrationFormId, CreateParticipationsFromRegistrationsResult convertionResult); 
    }
}
