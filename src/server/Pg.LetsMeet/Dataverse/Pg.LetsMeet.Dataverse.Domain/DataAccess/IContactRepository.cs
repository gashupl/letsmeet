using Microsoft.Xrm.Sdk;
using Pg.LetsMeet.Dataverse.Context;
using System;

namespace Pg.LetsMeet.Dataverse.Domain.DataAccess
{
    public interface IContactRepository : IRepository
    {
        EntityReference GetParentCustomerRef(Guid contactId);
        Contact GetContactByEmail(string email); 
    }
}
