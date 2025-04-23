using Microsoft.Xrm.Sdk;
using Microsoft.PowerPlatform.Dataverse.Client;
using System.Diagnostics.CodeAnalysis;
using Pg.LetsMeet.Api.Common.Services;

namespace Pg.LetsMeet.Api.Common.Connections
{
    [ExcludeFromCodeCoverage]
    public class DataverseConnector : IDataSourceConnector
    {
        private readonly IConfigurationService _config; 
        public DataverseConnector(IConfigurationService config)
        {
            _config = config;
        }

        public IOrganizationService Create()
        {
            var config = new ConfigModel()
            {
                BaseUrl = _config.GetValue("crm-base-url"),
                ClientId = _config.GetValue("crm-application-id"),
                ClientSecret = _config.GetValue("crm-client-secret")
            }; 

            return Create(config); 
        }

        private static IOrganizationService Create(ConfigModel config)
        {
            string connectionString = $"AuthType='ClientSecret';ServiceUri='{config.BaseUrl}';ClientId = '{config.ClientId}';ClientSecret = '{config.ClientSecret}';";
            var serviceClient = new ServiceClient(connectionString);
            return (IOrganizationService)serviceClient;
        }
    }
}
