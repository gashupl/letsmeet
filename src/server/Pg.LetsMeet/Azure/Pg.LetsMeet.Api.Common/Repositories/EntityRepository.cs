using Microsoft.Xrm.Sdk;

namespace Pg.LetsMeet.Api.Common.Repositories
{
    public class EntityRepository : RepositoryBase<Entity>
    {
        public EntityRepository(IOrganizationServiceFactory factory, Guid? userId) : base(factory, userId)
        {
        }
    }
}
