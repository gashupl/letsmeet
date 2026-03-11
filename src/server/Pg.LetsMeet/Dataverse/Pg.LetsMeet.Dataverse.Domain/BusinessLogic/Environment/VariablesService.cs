using Microsoft.Xrm.Sdk;
using Pg.LetsMeet.Dataverse.Domain.DataAccess;
using Pg.LetsMeet.Dataverse.Shared.Services;

namespace Pg.LetsMeet.Dataverse.Domain.BusinessLogic.Environment
{
    public class VariablesService : ServiceBase, IVariablesService
    {
        private IEnvVariablesRepository _variablesRepository; 
        public VariablesService(IRepositoriesFactory repositoryFactory, IPluginTracingService tracing) : base(repositoryFactory, tracing)
        {
            _variablesRepository = repositoryFactory.Get<IEnvVariablesRepository>(); 
        }

        public string GetSampleUrl()
        {
            return _variablesRepository.GetDefaultValue("pg_samplesiteurl"); 
        }
    }
}
