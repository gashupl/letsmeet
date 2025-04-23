using Microsoft.Xrm.Sdk;
using PostDeploymentStepsRunner.Model;

namespace PostDeploymentStepsRunner.Services
{
    internal interface IDeploymentStepService

    {
        IDeploymentStepService NextService { get; set; }
        bool StopProcessintOnError { get; set; }
        IDeploymentStepService SetNext(IDeploymentStepService service, bool stopProcessintOnError); 

        DeploymentStepResult Execute(IOrganizationService service);
    }
}