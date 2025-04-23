using Pg.LetsMeet.Identity.Domain.Data;
using Pg.LetsMeet.Identity.Domain.Model;

namespace Pg.LetsMeet.Identity.Domain.Services
{
    public class PartnerService : IPartnerService
    {
        private readonly IAccountRepository _accountRepository;
        public PartnerService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public IsValidPartnerResponse IsValidPartner(string email)
        {
            var account = _accountRepository.FindByEmail(email);

            if (account == null)
            {
                return new IsValidPartnerResponse { IsValid = false, ErrorMessage = "Account not found" };
            }
            else
            {
                return new IsValidPartnerResponse { IsValid = true, PartnerId = account.AccountNumber };
            }
        }
    }
}
