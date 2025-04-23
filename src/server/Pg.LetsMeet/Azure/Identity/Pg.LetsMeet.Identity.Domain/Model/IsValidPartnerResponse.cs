namespace Pg.LetsMeet.Identity.Domain.Model
{
    public class IsValidPartnerResponse
    {
        public bool IsValid { get; set; }
        public string PartnerId { get; set; }
        public string ErrorMessage { get; set; }
    }
}
