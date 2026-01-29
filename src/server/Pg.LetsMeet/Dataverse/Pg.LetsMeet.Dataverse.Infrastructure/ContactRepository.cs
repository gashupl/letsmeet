using Microsoft.Xrm.Sdk;
using Pg.LetsMeet.Dataverse.Context;
using Pg.LetsMeet.Dataverse.Domain.DataAccess;
using System;
using System.Linq;

namespace Pg.LetsMeet.Dataverse.Infrastructure
{
    public class ContactRepository : RepositoryBase, IContactRepository
    {
        public EntityReference GetParentCustomerRef(Guid contactId)
        {
            using (var context = CreateContext<DataverseContext>())
            {
                var query = context.ContactSet
                    .Where(c => c.Id == contactId)
                    .Select(c => c.ParentCustomerId);

                return query.FirstOrDefault();
            }
        }

        public Contact GetContactByEmail(string email)
        {
            using (var context = CreateContext<DataverseContext>())
            {
                var query = context.ContactSet
                    .Where(c => c.EMailAddress1 != null && c.EMailAddress1 == email);
                return query.FirstOrDefault();
            }
        }
    }
}
