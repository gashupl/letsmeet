using Microsoft.Xrm.Sdk;
using Pg.LetsMeet.Api.Common.Repositories;
using Pg.LetsMeet.Dataverse.Context;
using Pg.LetsMeet.Identity.Domain.Data; 

namespace Pg.LetsMeet.Identity.Infrastructure.Repositories
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(IOrganizationServiceFactory factory, Guid? userId) : base(factory, userId)
        {
        }

        public Account? FindByEmail(string email)
        {
            using (var context = new DataverseServiceContext(service))
            {
                var query = context.AccountSet
                    .Where(a => a.EMailAddress1 == email && a.StateCode == AccountState.Active)
                    .Select(a => a);

                return query.FirstOrDefault<Account>();
            }
        }
    }
}
