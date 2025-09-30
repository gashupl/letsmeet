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
    }
}
