using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk;
using System.Linq.Expressions;

namespace Pg.LetsMeet.Api.Common.Repositories
{
    public interface IRepository
    {
        IRepository AsAdmin();

        IRepository AsUser(Guid? userId = null);

        TResponse Execute<VRequest, TResponse>(VRequest request)
            where VRequest : OrganizationRequest
            where TResponse : OrganizationResponse;

        EntityMetadata RetrieveEntityMetadata(string entityLogicalName);

        void UpdateEntity(EntityMetadata entityMetadata);

        void PublishEntity(string entityLogicalName);
    }

    public interface IRepository<T> : IRepository
        where T : Entity, new()
    {
        Guid Create(T entity);

        void Update(T entity);

        void Delete(T entity);

        T Retrieve(Guid id, params string[] columns);

        T Retrieve(string entityLogicalName, Guid id, params string[] columns);

        T Retrieve(Guid id, Expression<Func<T, T>> constructor);

        List<T> RetrieveAll(params string[] columns);

        U CustomRetrieve<U>(Func<OrganizationServiceContext, U> customRetriever);

        void SetActivationState(T entity, int stateCode, int statusCode);

        void Assign(T entity, EntityReference newOwner);

        void Associate(T entity, string relationshipName, EntityReference toAssociate);

        void Associate(T entity, string relationshipName, EntityReferenceCollection relatedEntities);

        void Associate(T entity, Relationship relationship, EntityReferenceCollection relatedEntities);

        void Disassociate(T entity, string relationshipName, EntityReference toDisassociate);

        void Disassociate(T entity, string relationshipName, EntityReferenceCollection relatedEntities);

        void Disassociate(T entity, Relationship relationship, EntityReferenceCollection relatedEntities);

        void GrantAccess(T entity, EntityReference principal, AccessRights accessRights);

        void RevokeAccess(T entity, EntityReference principal);

        Guid AddToQueue(T entity, Guid destinationQueueId, Guid? sourceQueueId = null);
    }
}
