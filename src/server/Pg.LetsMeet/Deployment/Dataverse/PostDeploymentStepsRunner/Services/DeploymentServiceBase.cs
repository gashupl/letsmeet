using Microsoft.Xrm.Sdk;
using PostDeploymentStepsRunner.Model;

namespace PostDeploymentStepsRunner.Services
{
    internal abstract class DeploymentServiceBase : IDeploymentStepService
    {
        public IDeploymentStepService NextService { get; set; }
        public bool StopProcessintOnError { get; set; }

        public abstract DeploymentStepResult Execute(IOrganizationService service); 

        public IDeploymentStepService SetNext(IDeploymentStepService service, bool stopProcessintOnError)
        {
            NextService = service; 
            StopProcessintOnError = stopProcessintOnError;
            return NextService; 
        }
    }
}
