namespace Pg.LetsMeet.Dataverse.Domain.BusinessLogic.Contact
{
    public class ContactExistsResponse
    {
        public bool Exists { get; set; }
        public Context.Contact Contact { get; set; }
    }
}
