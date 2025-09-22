using Microsoft.Xrm.Sdk;
using System;

namespace Pg.LetsMeet.Dataverse.Domain.DataAccess
{
    public interface IContactRepository : IRepository
    {
        EntityReference GetParentCustomerRef(Guid contactId); 
    }
}
