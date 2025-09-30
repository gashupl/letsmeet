using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Pg.LetsMeet.Dataverse.Context;
using PostDeploymentStepsRunner.Data;
using PostDeploymentStepsRunner.Model;
using System.ServiceModel;

namespace PostDeploymentStepsRunner.Services
{
    internal class UpsertSettingsService : DeploymentServiceBase , IUpsertSettingsService
    {

        private readonly bool _showWelcomeScreen;
        private readonly string _appModuleName = "Let's meet"; 

        public UpsertSettingsService(UpsertSettingsConfig config)
        {
            _showWelcomeScreen = config.ShowWelcomeScreen; 
        }

        public override DeploymentStepResult Execute(IOrganizationService service)
        {
            var newAppSetting = new AppSetting();

            using (var context = new DataverseContext(service))
            {
                try
                {

                    var query = from a in context.AppSettingSet
                                join d in context.SettingDefinitionSet
                                on a.SettingDefinitionId.Id equals d.Id 
                                where d.UniqueName == SettingsName.WelcomeScreen
                                select new { AppSetting = a, SettingDefinition = d };

                    var appSetting = query.FirstOrDefault();
                    if (appSetting != null)
                    {
                        newAppSetting.Id = appSetting.AppSetting.Id;
                        newAppSetting.SettingDefinitionId = appSetting.SettingDefinition.ToEntityReference();
                    }
                    else
                    {

                        var appQuery = from a in context.AppModuleSet
                                       where a.Name == _appModuleName
                                       select a;
                        var app = appQuery.FirstOrDefault(); 
                        if(app != null)
                        {
                            newAppSetting.ParentAppModuleId = app.ToEntityReference();
                        }           
                        newAppSetting.SettingDefinitionId = context.SettingDefinitionSet
                            .Where(d => d.UniqueName == SettingsName.WelcomeScreen)
                            .FirstOrDefault()?.ToEntityReference(); 
                    }
                    newAppSetting.Value = _showWelcomeScreen.ToString().ToLower();
                    service.Execute(new UpsertRequest
                    {
                        Target = newAppSetting
                    });
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
}
