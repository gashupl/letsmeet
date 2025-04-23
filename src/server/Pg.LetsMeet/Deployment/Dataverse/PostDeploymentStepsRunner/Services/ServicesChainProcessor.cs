using Microsoft.Extensions.Logging;
using Microsoft.Xrm.Sdk;


namespace PostDeploymentStepsRunner.Services
{
    internal class ServicesChainProcessor
    {
        private readonly IOrganizationService _client;
        private readonly ILogger _logger;

        public ServicesChainProcessor(IOrganizationService client, ILogger logger)
        {
            _client = client;
            _logger = logger;
        }

        public void Process(IDeploymentStepService service)
        {
            var executedService = service;
            do
            {
                _logger.LogInformation($"Processing { executedService.GetType() }"); 
               var result =  executedService.Execute(_client);
                if (!result.IsSuccess)
                {
                    _logger.LogError(result.Message);
                    if (executedService.StopProcessintOnError)
                    {
                        break; 
                    }
                }

                executedService = executedService.NextService;

            } 
            while (executedService != null);
        }
    }
}
