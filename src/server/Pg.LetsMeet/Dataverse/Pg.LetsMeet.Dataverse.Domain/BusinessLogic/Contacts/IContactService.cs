using System;

namespace Pg.LetsMeet.Dataverse.Domain.BusinessLogic.Contacts
{
    public interface IContactService : IService
    {
        ContactExistsResponse ContactExists(string email);
        bool UpdateContactIfChanged(Context.Contact contact, string firstName, string lastName);

        Guid UpsertContactWithEmail(string email, string firstName, string lastName); 
    }
}
