using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using PostDeploymentStepsRunner.Data;
using PostDeploymentStepsRunner.Model;
using PostDeploymentStepsRunner.Services;

public class Program
{
    public static void Main(string[] args)
    {
        var argsReader = new InputArgumentReader();
        ILogger logger;
        InputDto input;
        try
        {
            input = argsReader.GetInput(args);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine($"Required parameters:");
            Console.WriteLine($"{InputArgumentReader.UrlPrefix}value");
            Console.WriteLine($"{InputArgumentReader.AppIdPrefix}value");
            Console.WriteLine($"{InputArgumentReader.ClientSecretPrefix}value");
            return;
        }

        var connectionString = @$"Url={input.DataverseUrl};AuthType=ClientSecret;"
                + $"ClientId={input.ApplicationId};ClientSecret={input.ClientSecret};RequireNewInstance=true";

        var client = new ServiceClient(connectionString);

        var serviceProvider = new ServiceCollection()
            .AddLogging(opt => opt.AddConsole().SetMinimumLevel(LogLevel.Trace))
            .AddSingleton<IOrganizationService>(client)
            .AddSingleton<IUpsertSettingsService>(s => new UpsertSettingsService(
                new UpsertSettingsConfig
                    {
                        ShowWelcomeScreen = input.WelcomeScreenSettingValue
                    }))
             .AddSingleton<IPublishCustomizationsService, PublishCustomizationsService>()
            .BuildServiceProvider();

        var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
        if (loggerFactory != null)
        {
            logger = loggerFactory.CreateLogger<Program>();
            try
            {

                logger.LogInformation("Starting application");

                var publishService = serviceProvider.GetService<IPublishCustomizationsService>(); 
                var upsertSettingsService = serviceProvider.GetService<IUpsertSettingsService>();

                upsertSettingsService.SetNext(publishService, true);
                new ServicesChainProcessor(client, logger).Process(upsertSettingsService); 

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unhandled exception occured.");
            }

            logger.LogInformation("Closing application");
        }
    }
}