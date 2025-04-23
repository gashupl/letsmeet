using Microsoft.Xrm.Sdk;

namespace Pg.LetsMeet.Api.Common.Connections
{
    public interface IDataSourceConnector
    {
        IOrganizationService Create(); 
    }
}
