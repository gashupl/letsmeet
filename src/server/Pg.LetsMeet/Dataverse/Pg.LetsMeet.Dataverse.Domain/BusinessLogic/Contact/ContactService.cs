using Microsoft.Xrm.Sdk;
using Pg.LetsMeet.Dataverse.Domain.BusinessLogic.Environment;
using Pg.LetsMeet.Dataverse.Domain.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pg.LetsMeet.Dataverse.Domain.BusinessLogic.Contact
{
    public class ContactService : ServiceBase, IContactService
    {
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
    }
}
