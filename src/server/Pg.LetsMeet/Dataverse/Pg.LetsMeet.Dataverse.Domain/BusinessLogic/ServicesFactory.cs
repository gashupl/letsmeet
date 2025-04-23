using System;
using System.Linq;
using Microsoft.Xrm.Sdk;
using Pg.LetsMeet.Dataverse.Domain.DataAccess;
using Pg.LetsMeet.Dataverse.Shared.Injections;

namespace Pg.LetsMeet.Dataverse.Domain
{
    public class ServicesFactory : IServicesFactory
    {
        protected virtual IContainer Container { get; }

        public ServicesFactory(IContainer container)
        {
            Container = container;
        }
        public T Get<T>() where T : class
        {
            return Container.GetInstance<T>(); 
        }
    }
}
