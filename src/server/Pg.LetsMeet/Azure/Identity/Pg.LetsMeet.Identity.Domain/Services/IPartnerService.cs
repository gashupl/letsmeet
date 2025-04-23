using Pg.LetsMeet.Identity.Domain.Model;

namespace Pg.LetsMeet.Identity.Domain.Services
{
    public interface IPartnerService
    {
        IsValidPartnerResponse IsValidPartner(string email); 
    }
}
