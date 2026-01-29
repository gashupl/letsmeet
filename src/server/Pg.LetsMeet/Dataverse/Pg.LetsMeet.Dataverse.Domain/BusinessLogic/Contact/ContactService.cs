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

        public bool ContactExists(string email)
        {
            var contactRepository = repositoryFactory.Get<IContactRepository>();
            var contact = contactRepository.GetContactByEmail(email);
            return contact != null;
        }
    }
}
