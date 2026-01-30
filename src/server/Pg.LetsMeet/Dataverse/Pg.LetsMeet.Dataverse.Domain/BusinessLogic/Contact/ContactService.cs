using Microsoft.Xrm.Sdk;
using Pg.LetsMeet.Dataverse.Domain.DataAccess;
using System;

namespace Pg.LetsMeet.Dataverse.Domain.BusinessLogic.Contact
{
    public class ContactService : ServiceBase, IContactService
    {
        private readonly IContactRepository _contactRepository;
        public ContactService(IRepositoriesFactory repositoryFactory, ITracingService tracing) : base(repositoryFactory, tracing)
        {
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
            var contactExistsResponse = ContactExists(email);
            if (contactExistsResponse.Exists)
            {
                UpdateContactIfChanged(
                    contactExistsResponse.Contact,
                    firstName,
                    lastName);

                return contactExistsResponse.Contact.Id;
            }
            else
            {
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
