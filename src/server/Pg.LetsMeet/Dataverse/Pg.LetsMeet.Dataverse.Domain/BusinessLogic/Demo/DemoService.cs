using System; 
using Microsoft.Xrm.Sdk;
using Pg.LetsMeet.Dataverse.Context;
using Pg.LetsMeet.Dataverse.Domain.DataAccess;

namespace Pg.LetsMeet.Dataverse.Domain.BusinessLogic.Demo
{
    public class DemoService : ServiceBase, IDemoService
    {
        public DemoService(IRepositoriesFactory repositoryFactory, ITracingService tracing) : base(repositoryFactory, tracing)
        { 
        }

        public string DoSomething(string name, Guid userId)
        {
            tracing.Trace($"Do something method executed");

            var accountRepository = repositoryFactory.Get<IAccountRepository>(userId);

            var accountName = $"Plugin test { name } account created on: { DateTime.Now }";
            accountRepository.Create(new Account() { Name = name }); 

            tracing.Trace($"Do something method completed");

            return accountName;
        }
    }
}
