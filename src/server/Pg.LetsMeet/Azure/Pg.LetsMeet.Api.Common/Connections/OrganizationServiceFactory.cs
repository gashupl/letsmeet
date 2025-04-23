using Microsoft.Xrm.Sdk;

namespace Pg.LetsMeet.Api.Common.Connections
{
    public class OrganizationServiceFactory : IOrganizationServiceFactory
    {
        private readonly IDataSourceConnector _connector;
        private readonly IOrganizationService _service; 
        public OrganizationServiceFactory(IDataSourceConnector connector)
        {
            _connector = connector; 
        }

        public OrganizationServiceFactory(IOrganizationService service)
        {
            _service = service; 
        }

        public IOrganizationService CreateOrganizationService(Guid? userId)
        {
            if(_service != null)
            {
                return _service; 
            }
            else
            {
                if(_connector != null)
                {
                    return _connector.Create();
                }
                else
                {
                    throw new InvalidOperationException("Cannot create Organization service"); 
                }
            }
        }
    }
}
