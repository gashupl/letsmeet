using Microsoft.Xrm.Sdk;
using Pg.LetsMeet.Dataverse.Domain.DataAccess;
using Pg.LetsMeet.Dataverse.Shared.Services;

namespace Pg.LetsMeet.Dataverse.Domain
{
    public abstract class ServiceBase : IService
    {
        protected readonly IRepositoriesFactory repositoryFactory; 
        protected readonly IPluginTracingService tracing; 

        public ServiceBase(IRepositoriesFactory repositoryFactory, IPluginTracingService tracing)
        {
            this.repositoryFactory = repositoryFactory;
            this.tracing = tracing; 
        }
    }
}
