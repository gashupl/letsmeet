namespace Pg.LetsMeet.Dataverse.Domain.BusinessLogic.Contact
{
    public interface IContactService : IService
    {
        ContactExistsResponse ContactExists(string email);
        bool UpdateContactIfChanged(Context.Contact contact, string firstName, string lastName);
    }
}
