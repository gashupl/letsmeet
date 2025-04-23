using Pg.LetsMeet.Dataverse.Domain.DataAccess;
using Pg.LetsMeet.Dataverse.Infrastructure;
using Pg.LetsMeet.Dataverse.Domain;
using Pg.LetsMeet.Dataverse.Domain.BusinessLogic.Demo;
using Pg.LetsMeet.Dataverse.Domain.BusinessLogic.Environment;
using Pg.LetsMeet.Dataverse.Domain.BusinessLogic.EventParticipations;
using Microsoft.Xrm.Sdk;
using Pg.LetsMeet.Dataverse.Shared.Injections;
using System;

namespace Pg.LetsMeet.Dataverse.Plugins.Plugins
{
    internal class CalculateEventParticipantsDependencyLoader : IDependencyLoader
    {
        public void SetRegistrations(IContainer container)
        {
            _ = container ?? throw new ArgumentNullException(nameof(container));

            container.Register<IRepository, RepositoryBase>();
            container.Register<IAccountRepository, AccountRepository>();
            container.Register<IEnvVariablesRepository, EnvVariablesRepository>();
            container.Register<IEventParticipationRepository, EventParticipationRepository>();

            container.Register<IDemoService, DemoService>();
            container.Register<IVariablesService, VariablesService>();
            container.Register<IEventParticipationService, EventParticipationService>();

        }
    }
}
