using Pg.LetsMeet.Api.Common.Repositories;
using Pg.LetsMeet.Dataverse.Context;

namespace Pg.LetsMeet.Events.Domain.Data
{
    public interface IAccountRepository : IRepository<Account>
    {
        Account? FindByAccountCode(string accountCode);
    }
}