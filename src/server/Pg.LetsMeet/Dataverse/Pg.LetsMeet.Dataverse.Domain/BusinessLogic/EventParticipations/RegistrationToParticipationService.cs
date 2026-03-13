using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.PluginTelemetry;
using Pg.LetsMeet.Dataverse.Context;
using Pg.LetsMeet.Dataverse.Domain.BusinessLogic.Contacts;
using Pg.LetsMeet.Dataverse.Domain.DataAccess;
using Pg.LetsMeet.Dataverse.Shared.Services;
using System;
using System.ServiceModel;

namespace Pg.LetsMeet.Dataverse.Domain.BusinessLogic.EventParticipations
{
    public class RegistrationToParticipationService : ServiceBase, IRegistrationToParticipationService
    {
        private readonly IContactService _contactService;
        private readonly IEventParticipationRepository _eventParticipationRepository;
        private readonly IRepository _repository; 

        public RegistrationToParticipationService(IRepositoriesFactory repositoryFactory, IPluginTracingService tracing, IContactService contactService) 
            : base(repositoryFactory, tracing)
        {
            _contactService = contactService;
            _repository = repositoryFactory.Get<IRepository>();
            _eventParticipationRepository = repositoryFactory.Get<IEventParticipationRepository>();
        }

        public CreateParticipationsFromRegistrationsResult TryCreateParticipationsFromRegistrations(pg_eventregistrationform registrationForm)
        {
            tracing.Trace(LogLevel.Information, 
                "Starting participation creation for registration form: {registrationFormId}, Email: {email}", 
                registrationForm.Id, registrationForm.pg_email);

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
                tracing.Trace(LogLevel.Information, 
                    "Event participation created successfully. ContactId: {contactId}, EventId: {eventId}, RegistrationFormId: {registrationFormId}",
                    contactId, registrationForm.pg_eventId?.Id, registrationForm.Id);
                return CreateParticipationsFromRegistrationsResult.Success;
            }
            catch (FaultException<OrganizationServiceFault> ex)
            {
                tracing.Trace(LogLevel.Error, 
                    "[FaultException] Failed to create event participation: {exceptionMessage}", ex.Message);

                if(ex.Detail != null)
                {
                    tracing.Trace(LogLevel.Error, 
                        "OrganizationServiceFault Detail - ErrorCode: {errorCode}, Message: {faultMessage}", 
                        ex.Detail.ErrorCode, ex.Detail.Message);
                }
                return CreateParticipationsFromRegistrationsResult.Failure; 
            }
            catch(Exception ex)
            {
                tracing.Trace(LogLevel.Error, 
                    "[Exception] Failed to create event participation: {exceptionMessage}", ex.Message);
                return CreateParticipationsFromRegistrationsResult.Failure;
            }

        }

        public void CloseRegistrationForm(Guid registrationFormId, CreateParticipationsFromRegistrationsResult convertionResult)
        {
            if(convertionResult == CreateParticipationsFromRegistrationsResult.Success)
            {      
                _repository.UpdateState(registrationFormId,
                    pg_eventregistrationform.EntityLogicalName,
                    (int)pg_eventregistrationform_statecode.Inactive, 
                    (int)pg_eventregistrationform_StatusCode.Accepted);
                tracing.Trace(LogLevel.Information, 
                    "Registration form accepted - RegistrationFormId: {registrationFormId}", registrationFormId);
            }
            else
            {       
                _repository.UpdateState(registrationFormId,
                    pg_eventregistrationform.EntityLogicalName,
                    (int)pg_eventregistrationform_statecode.Inactive, 
                    (int)pg_eventregistrationform_StatusCode.Rejected);
                tracing.Trace(LogLevel.Warning, 
                    "Registration form rejected - RegistrationFormId: {registrationFormId}", registrationFormId);
            }
        }
    }
}
