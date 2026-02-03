using Pg.LetsMeet.Dataverse.Domain.BusinessLogic.Contacts;
using Pg.LetsMeet.Dataverse.Domain.DataAccess;
using Pg.LetsMeet.Dataverse.Infrastructure;
using Pg.LetsMeet.Dataverse.Shared.Injections;

namespace Pg.LetsMeet.Dataverse.Plugins.Events
{
    internal class TryConvertRegistrationToParticipationDependencyLoader : IDependencyLoader
    {
        public void SetRegistrations(IContainer container)
        {
            container.Register<IRepository, RepositoryBase>();
            container.Register<IContactRepository, ContactRepository>();
            container.Register<IEventParticipationRepository, EventParticipationRepository>();
            container.Register<IContactService, ContactService>();
        }
    }
}
