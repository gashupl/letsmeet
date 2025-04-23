using Pg.LetsMeet.Api.Common.Repositories;
using Pg.LetsMeet.Dataverse.Context;

namespace Pg.LetsMeet.Events.Domain.Data
{
    public interface IEventRepository : IRepository<pg_event>
    {
        IList<pg_event> FindByAccountCode(string accountCode); 
    }
}
