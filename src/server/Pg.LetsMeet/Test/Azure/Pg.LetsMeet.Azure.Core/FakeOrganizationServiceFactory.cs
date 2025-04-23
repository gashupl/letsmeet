using Microsoft.Xrm.Sdk;

namespace Pg.LetsMeet.Azure.Core
{
    public class FakeOrganizationServiceFactory : IOrganizationServiceFactory
    {
        private readonly IOrganizationService _service;
        public FakeOrganizationServiceFactory(IOrganizationService service)
        {
            _service = service;
        }
        public IOrganizationService CreateOrganizationService(Guid? userId)
        {
            return _service;
        }
    }
}
