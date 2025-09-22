using Pg.LetsMeet.Dataverse.Domain.BusinessLogic.Event;
using Pg.LetsMeet.Dataverse.Domain.DataAccess;
using Pg.LetsMeet.Dataverse.Infrastructure;
using Pg.LetsMeet.Dataverse.Shared.Injections;
using System;

namespace Pg.LetsMeet.Dataverse.Plugins.Plugins
{
    internal class SetPartnerOnNewEventDependencyLoader : IDependencyLoader
    {
        public void SetRegistrations(IContainer container)
        {
            _ = container ?? throw new ArgumentNullException(nameof(container));

            container.Register<IRepository, RepositoryBase>();
            container.Register<IContactRepository, ContactRepository>();

            container.Register<IEventService, EventService>();
        }
    }
}
