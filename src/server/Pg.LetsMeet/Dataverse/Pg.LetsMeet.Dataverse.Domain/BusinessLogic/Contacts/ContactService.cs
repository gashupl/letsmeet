using Microsoft.Xrm.Sdk.PluginTelemetry;
using Pg.LetsMeet.Dataverse.Domain.DataAccess;
using Pg.LetsMeet.Dataverse.Shared.Services;
using System;

namespace Pg.LetsMeet.Dataverse.Domain.BusinessLogic.Contacts
{
    public class ContactService : ServiceBase, IContactService
    {
        private readonly IContactRepository _contactRepository;
        public ContactService(IRepositoriesFactory repositoryFactory, IPluginTracingService tracing) : base(repositoryFactory, tracing)
        {
            _contactRepository = repositoryFactory.Get<IContactRepository>();
        }

        public ContactExistsResponse ContactExists(string email)
        {
            var contactRepository = repositoryFactory.Get<IContactRepository>();
            var contact = contactRepository.GetContactByEmail(email);
            return new ContactExistsResponse
            {
                Exists = contact != null,
                Contact = contact
            };
        }

        public bool UpdateContactIfChanged(Context.Contact contact, string firstName, string lastName)
        {

            var contactRepository = repositoryFactory.Get<IContactRepository>();

            bool isUpdated = false;
            if (contact.FirstName != firstName)
            {
                contact.FirstName = firstName;
                isUpdated = true;
            }
            if (contact.LastName != lastName)
            {
                contact.LastName = lastName;
                isUpdated = true;
            }
            if (isUpdated)
            {
                contactRepository.Update(contact);
            }
            return isUpdated;
        }

        public Guid UpsertContactWithEmail(string email, string firstName, string lastName)
        {
            tracing.Trace(LogLevel.Trace, "UpsertContactWithEmail called with email: {0}, firstName: {1}, lastName: {2}", email, firstName, lastName);
            var contactExistsResponse = ContactExists(email);
            if (contactExistsResponse.Exists)
            {
                tracing.Trace(LogLevel.Trace, "Contact with email {0} already exists. Checking for updates.", email);
                UpdateContactIfChanged(
                    contactExistsResponse.Contact,
                    firstName,
                    lastName);

                return contactExistsResponse.Contact.Id;
            }
            else
            {
                tracing.Trace(LogLevel.Trace, "Contact with email {0} does not exist. Creating new contact.", email);
                var newContact = new Context.Contact
                {
                    FirstName = firstName,
                    LastName = lastName,
                    EMailAddress1 = email
                };
                return _contactRepository.Create(newContact);
            }
        }
    }
}
