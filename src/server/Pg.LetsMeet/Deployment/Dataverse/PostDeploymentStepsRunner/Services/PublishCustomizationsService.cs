using Microsoft.Xrm.Sdk;
using PostDeploymentStepsRunner.Model;
using Microsoft.Crm.Sdk.Messages;
using System.ServiceModel;

namespace PostDeploymentStepsRunner.Services
{
    internal class PublishCustomizationsService : DeploymentServiceBase , IPublishCustomizationsService
    {
        public override DeploymentStepResult Execute(IOrganizationService service)
        {
            try
            {
                var request = new PublishAllXmlRequest();
                service.Execute(request);
            }
            catch (FaultException<OrganizationServiceFault> ex)
            {
                return new DeploymentStepResult()
                {
                    Message = ex.Detail.Message
                };
            }
            catch (Exception ex)
            {
                return new DeploymentStepResult()
                {
                    Message = (ex.InnerException?.Message ?? ex.Message)
                };
            }

            return new DeploymentStepResult { IsSuccess = true };

        }
    }
}
