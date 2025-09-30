using Microsoft.Xrm.Sdk;
using Pg.LetsMeet.Api.Common.Repositories;
using Pg.LetsMeet.Dataverse.Context;
using Pg.LetsMeet.Events.Domain.Data;

namespace Pg.LetsMeet.Events.Infrastructure.Repositories
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(IOrganizationServiceFactory factory, Guid? userId = null) : base(factory, userId)
        {
        }

        public AccountRepository(IOrganizationService service) : base(service)
        {
        }

        public Account? FindByAccountCode(string accountCode)
        {
            using (var context = new DataverseContext(service))
            {
                var query = context.AccountSet
                    .Where(a => a.AccountNumber == accountCode && a.StateCode == account_statecode.Active)
                    .Select(a => a);

                return query.FirstOrDefault<Account>();
            }
        }
    }
}
