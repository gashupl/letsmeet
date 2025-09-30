using Microsoft.Xrm.Sdk;
using Pg.LetsMeet.Dataverse.Shared.Injections;
using System;

namespace Pg.LetsMeet.Dataverse.Domain.DataAccess
{
    public class RepositoriesFactory : IRepositoriesFactory
    {
        private readonly IOrganizationServiceFactory serviceFactory;
        protected virtual IContainer Container { get; }

        public RepositoriesFactory(IContainer container)
        {
            Container = container;
            serviceFactory = Container.GetInstance<IOrganizationServiceFactory>(); 
        }

        public T Get<T>(Guid? userId = null) where T : IRepository
        {
            var instance = (T)Container.GetInstance(typeof(T));
            instance.Initialize(serviceFactory.CreateOrganizationService(userId));
            return instance;
        }
    }
}
