using Pg.LetsMeet.Dataverse.Domain;
using Pg.LetsMeet.Dataverse.Shared.Injections;
using System;

namespace Pg.LetsMeet.Dataverse.Plugins.Tests
{
    public class DependencyLoaderFakeBase
    {
        public IServicesFactory DomainServicesFactory;

        public void SetRegistrations(IContainer container)
        {
            throw new NotImplementedException();
        }
    }
}
