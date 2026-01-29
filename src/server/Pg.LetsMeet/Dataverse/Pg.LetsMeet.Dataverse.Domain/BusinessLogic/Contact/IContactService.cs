namespace Pg.LetsMeet.Dataverse.Domain.BusinessLogic.Contact
{
    public interface IContactService : IService
    {
        bool ContactExists(string email);
    }
}
