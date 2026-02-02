using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Pg.LetsMeet.Dataverse.Context;
using Pg.LetsMeet.Dataverse.Domain.BusinessLogic.Contacts;
using Pg.LetsMeet.Dataverse.Domain.DataAccess;
using System;

namespace Pg.LetsMeet.Dataverse.Domain.BusinessLogic.EventParticipations
{
    public class RegistrationToParticipationService : ServiceBase, IRegistrationToParticipationService
    {
        private readonly IContactService _contactService;
        private readonly IContactRepository _contactRepository;
        private readonly IEventParticipationRepository _eventParticipationRepository;
        private readonly IRepository _repository; 

        public RegistrationToParticipationService(IRepositoriesFactory repositoryFactory, ITracingService tracing, IContactService contactService) 
            : base(repositoryFactory, tracing)
        {
            _contactService = contactService;
            _repository = repositoryFactory.Get<IRepository>();
            _contactRepository = repositoryFactory.Get<IContactRepository>();
            _eventParticipationRepository = repositoryFactory.Get<IEventParticipationRepository>();
        }

        public CreateParticipationsFromRegistrationsResult TryCreateParticipationsFromRegistrations(pg_eventregistrationform registrationForm)
        {
            var contactId = _contactService.UpsertContactWithEmail(
                registrationForm.pg_email,
                registrationForm.pg_firstname,
                registrationForm.pg_lastname);

            try
            {
                var participation = new pg_eventparticipation
                {
                    pg_sourceregistrationformId = registrationForm.ToEntityReference(),
                    pg_contactId = new EntityReference(Context.Contact.EntityLogicalName, contactId),
                    pg_eventId = registrationForm.pg_eventId

                };
                _eventParticipationRepository.Create(participation);
                tracing.Trace("Event participation created successfully.");
                return CreateParticipationsFromRegistrationsResult.Success;
            }
            catch (InvalidPluginExecutionException ex)
            {
                tracing.Trace("Failed to create event participation: " + ex.Message);
                return CreateParticipationsFromRegistrationsResult.Failure; 
            }

        }

        public void CloseRegistrationForm(Guid registrationFormId, CreateParticipationsFromRegistrationsResult convertionResult)
        {
            if(convertionResult == CreateParticipationsFromRegistrationsResult.Success)
            {      
                _repository.UpdateState(registrationFormId, 
                    Context.pg_eventregistrationform.EntityLogicalName,
                    (int)pg_eventregistrationform_statecode.Inactive, 
                    (int)pg_eventregistrationform_StatusCode.Accepted);
                tracing.Trace("Registration form accepted");
            }
            else
            {       
                _repository.UpdateState(registrationFormId, 
                    Context.pg_eventregistrationform.EntityLogicalName,
                    (int)pg_eventregistrationform_statecode.Inactive, 
                    (int)pg_eventregistrationform_StatusCode.Rejected);
                tracing.Trace("Registration form rejected");
            }
        }
    }
}
