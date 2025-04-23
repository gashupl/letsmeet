using Azure.Core;
using Azure.Identity;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Pg.LetsMeet.Api.Common.Connections;
using Pg.LetsMeet.Api.Common.Services;
using Pg.LetsMeet.Events.Domain.Data;
using Pg.LetsMeet.Events.Domain.Services;
using Pg.LetsMeet.Events.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;

[assembly: FunctionsStartup(typeof(Pg.LetsMeet.Events.Api.Startup))]
namespace Pg.LetsMeet.Events.Api
{
    internal class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            
            var environment = Environment.GetEnvironmentVariable("ENVIRONMENT");
            if (environment != null && environment.ToLowerInvariant() == "local")
            {
                builder.Services.AddHttpClient();

                //Setup connection for local run
                builder.Services.AddScoped<IDataSourceConnector, DataverseConnector>();
                builder.Services.AddScoped<IOrganizationServiceFactory >(provider =>
                {
                    var connector = provider.GetService<IDataSourceConnector>();
                    return new OrganizationServiceFactory(connector); 
                });
            }
            else
            {
                //Setup connection for Azure Managed Identity 

                var identityClientId = Environment.GetEnvironmentVariable("dataverse-identity-id"); 
                var credentials = new ManagedIdentityCredential(identityClientId); 
                
                builder.Services.AddSingleton(credentials);     
                builder.Services.AddMemoryCache();
                builder.Services.AddSingleton(new DefaultAzureCredential());

                builder.Services.AddHttpClient("PowerAppsClient", async (provider, httpClient) =>
                {
                    var managedIdentity = provider.GetRequiredService<ManagedIdentityCredential>();

                    //var managedIdentity = provider.GetRequiredService<DefaultAzureCredential>();
                    var environment = provider.GetService<IConfigurationService>().GetValue("crm-base-url");
                    var cache = provider.GetService<IMemoryCache>();
                    httpClient.BaseAddress = new Uri($"{environment}/api/data/v9.2/");
                    httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
                    httpClient.DefaultRequestHeaders.Add("OData-MaxVersion", "4.0");
                    httpClient.DefaultRequestHeaders.Add("OData-Version", "4.0");
                    httpClient.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "azurefunction-powerapps");
                    httpClient.DefaultRequestHeaders.Add(HeaderNames.Authorization, (await GetToken(provider, environment, managedIdentity, cache)));

                });

                builder.Services.AddScoped<IOrganizationService>(provider =>
                {
                    var managedIdentity = provider.GetRequiredService<ManagedIdentityCredential>();
                   // var managedIdentity = provider.GetRequiredService<DefaultAzureCredential>();
                    var environment = provider.GetService<IConfigurationService>().GetValue("crm-base-url"); 
                    var cache = provider.GetService<IMemoryCache>();
                    return new ServiceClient(
                            tokenProviderFunction: f => GetToken(provider, environment, managedIdentity, cache),
                            instanceUrl: new Uri(environment),
                            useUniqueInstance: true);
                });

                builder.Services.AddScoped<IOrganizationServiceFactory>(provider =>
                {
                    var service = provider.GetService<IOrganizationService>();
                    return new OrganizationServiceFactory(service);
                });
            }
            builder.Services.AddScoped<IEventRepository, EventsRepository>();
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<IEventService, EventService>();
            builder.Services.AddScoped<IConfigurationService, ConfigurationService>();
            builder.Services.AddLogging();
        }

        private async Task<string> GetToken(IServiceProvider provider, string environment, DefaultAzureCredential credential, IMemoryCache cache)
        {
            var accessToken = await cache.GetOrCreateAsync(environment, async (cacheEntry) => {
                cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(50);
                var token =  (await credential.GetTokenAsync(new TokenRequestContext(new[] { environment }), default));
                return token;
            });
            
            return accessToken.Token;
        }

        private async Task<string> GetToken(IServiceProvider provider, string environment, ManagedIdentityCredential credential, IMemoryCache cache)
        {
            var accessToken = await cache.GetOrCreateAsync(environment, async (cacheEntry) => {
                cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(50);
                var token = (await credential.GetTokenAsync(new TokenRequestContext(new[] { environment }), default));
                return token;
            });

            return accessToken.Token;
        }


    }
}
