using Microsoft.Xrm.Sdk;
using Pg.LetsMeet.Dataverse.Domain.DataAccess;

namespace Pg.LetsMeet.Dataverse.Domain
{
    public abstract class ServiceBase : IService
    {
        protected readonly IRepositoriesFactory repositoryFactory; 
        protected readonly ITracingService tracing; 

        public ServiceBase(IRepositoriesFactory repositoryFactory, ITracingService tracing)
        {
            this.repositoryFactory = repositoryFactory;
            this.tracing = tracing; 
        }
    }
}
