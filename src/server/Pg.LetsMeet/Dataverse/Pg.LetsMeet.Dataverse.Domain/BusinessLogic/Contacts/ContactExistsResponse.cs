namespace Pg.LetsMeet.Dataverse.Domain.BusinessLogic.Contacts
{
    public class ContactExistsResponse
    {
        public bool Exists { get; set; }
        public Context.Contact Contact { get; set; }
    }
}
