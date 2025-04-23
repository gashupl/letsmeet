using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using System.Linq.Expressions;

namespace Pg.LetsMeet.Api.Common.Repositories
{
    public partial class RepositoryBase : IRepository
    {
        private readonly IOrganizationServiceFactory? factory;
        protected IOrganizationService service;
        protected IOrganizationService adminService;
        protected IOrganizationService userService;

        public RepositoryBase(IOrganizationServiceFactory factory, Guid? userId = null)
        {
            this.factory = factory;
            this.service = factory.CreateOrganizationService(Guid.Empty);
            this.userService = factory.CreateOrganizationService(userId);
            this.adminService = factory.CreateOrganizationService(null);
        }

        public RepositoryBase(IOrganizationService service)
        {
            this.service = service;
            this.userService = service;
            this.adminService = service; 
        }

        public IRepository AsAdmin()
        {
            this.service = this.adminService;
            return this;
        }

        public IRepository AsUser(Guid? userId = null)
        {
            if (!userId.HasValue || userId.Value == Guid.Empty)
            {
                this.service = this.userService;
            }
            else
            {
                this.service = this.factory?.CreateOrganizationService(userId.Value);
            }

            return this;
        }

        public TResponse Execute<VRequest, TResponse>(VRequest request)
            where TResponse : OrganizationResponse
            where VRequest : OrganizationRequest
        {
            return (TResponse)this.service.Execute(request);
        }

        public void PublishEntity(string entityLogicalName)
        {
            var publishEntityRequest = new PublishXmlRequest();
            publishEntityRequest.ParameterXml = $"<importexportxml><entities><entity>{entityLogicalName}</entity></entities></importexportxml>";
        }

        public EntityMetadata RetrieveEntityMetadata(string entityLogicalName)
        {
            var entityRequest = new RetrieveEntityRequest
            {
                EntityFilters = EntityFilters.All,
                LogicalName = entityLogicalName
            };

            return ((RetrieveEntityResponse)service.Execute(entityRequest)).EntityMetadata;
        }

        public void UpdateEntity(EntityMetadata entityMetadata)
        {
            var updateRequest = new UpdateEntityRequest
            {
                Entity = entityMetadata
            };

            service.Execute(updateRequest);
        }
    }

    public partial class RepositoryBase<T> : RepositoryBase
        where T : Entity, new()
    {
        public RepositoryBase(IOrganizationServiceFactory factory, Guid? userId) : base(factory, userId)
        {
        }

        public RepositoryBase(IOrganizationService service) : base(service)
        {
        }

        public Guid Create(T entity)
        {
            return this.service.Create(entity);
        }

        public void Update(T entity)
        {
            this.service.Update(entity);
        }

        public void Delete(T entity)
        {
            this.service.Delete(entity.LogicalName, entity.Id);
        }

        public T Retrieve(Guid id, params string[] columns)
        {
            var temp = new T();
            return Retrieve(temp.LogicalName, id, columns);
        }

        public T Retrieve(string entityLogicalName, Guid id, params string[] columns)
        {
            return this.service.Retrieve(
                entityLogicalName,
                id,
                columns != null && columns.Length > 0 ? new ColumnSet(columns) : new ColumnSet(true))
                .ToEntity<T>();
        }

        public T Retrieve(Guid id, Expression<Func<T, T>> constructor)
        {
            return this.CustomRetrieve(
                ctx =>
                {
                    return ctx.CreateQuery<T>()
                    .Where(x => x.Id == id)
                    .Select(constructor)
                    .Single();
                });
        }

        public U Retrieve<U>(Guid id, Expression<Func<T, U>> constructor)
        {
            using (var ctx = new OrganizationServiceContext(this.service) { MergeOption = Microsoft.Xrm.Sdk.Client.MergeOption.NoTracking })
            {
                return ctx.CreateQuery<T>()
                .Where(x => x.Id == id)
                .Select(constructor)
                .Single();
            }
        }

        public List<T> RetrieveAll(params string[] columns)
        {
            var paginator = new EntityCollectionPaginator<T>(this.service, columns);
            var entities = new List<T>();
            do
            {
                entities.AddRange(paginator.GetNextPage());
            } while (paginator.HasMore);

            return entities;
        }

        public List<T> RetrieveAll(IPaginatorSettings paginatorSettings)
        {
            var paginator = new EntityCollectionPaginator<T>(this.service, paginatorSettings);
            var entities = new List<T>();
            do
            {
                entities.AddRange(paginator.GetNextPage());
            } while (paginator.HasMore);

            return entities;
        }

        public U CustomRetrieve<U>(Func<OrganizationServiceContext, U> customRetriever)
        {
            using (var ctx = new OrganizationServiceContext(this.service) { MergeOption = Microsoft.Xrm.Sdk.Client.MergeOption.NoTracking })
            {
                return customRetriever.Invoke(ctx);
            }
        }

        public void SetActivationState(T entity, int stateCode, int statusCode)
        {
            SetStateRequest setStateRequest = new SetStateRequest();
            setStateRequest.EntityMoniker = entity.ToEntityReference();
            setStateRequest.State = new OptionSetValue(stateCode);
            setStateRequest.Status = new OptionSetValue(statusCode);
            this.service.Execute(setStateRequest);
        }

        public void Assign(T entity, EntityReference newOwner)
        {
            var updateOwnerEntity = new Entity(entity.LogicalName, entity.Id);
            updateOwnerEntity["ownerid"] = newOwner;
            this.service.Update(updateOwnerEntity);
        }

        public void Associate(T entity, string relationshipName, EntityReferenceCollection relatedEntities)
        {
            this.Associate(entity, new Relationship(relationshipName), relatedEntities);
        }

        public void Associate(T entity, Relationship relationship, EntityReferenceCollection relatedEntities)
        {
            this.service.Associate(entity.LogicalName, entity.Id, relationship, relatedEntities);
        }

        public void Associate(T entity, string relationshipName, EntityReference toAssociate)
        {
            var relatedEntities = new EntityReferenceCollection();
            relatedEntities.Add(toAssociate);
            this.Associate(entity, relationshipName, relatedEntities);
        }

        public void Disassociate(T entity, string relationshipName, EntityReference toDisassociate)
        {
            var relatedEntities = new EntityReferenceCollection();
            relatedEntities.Add(toDisassociate);
            this.Disassociate(entity, relationshipName, relatedEntities);
        }

        public void Disassociate(T entity, string relationshipName, EntityReferenceCollection relatedEntities)
        {
            this.Disassociate(entity, new Relationship(relationshipName), relatedEntities);
        }

        public void Disassociate(T entity, Relationship relationship, EntityReferenceCollection relatedEntities)
        {
            this.service.Disassociate(entity.LogicalName, entity.Id, relationship, relatedEntities);
        }

        public void GrantAccess(T entity, EntityReference principal, AccessRights accessRights)
        {
            this.service.Execute(new GrantAccessRequest()
            {
                Target = entity.ToEntityReference(),
                PrincipalAccess = new PrincipalAccess()
                {
                    Principal = principal,
                    AccessMask = accessRights,
                }
            });
        }

        public void RevokeAccess(T entity, EntityReference principal)
        {
            this.service.Execute(new RevokeAccessRequest()
            {
                Target = entity.ToEntityReference(),
                Revokee = principal,
            });
        }

        public Guid AddToQueue(T entity, Guid destinationQueueId, Guid? sourceQueueId = null)
        {
            var addToQueueRequest = new AddToQueueRequest();
            if (sourceQueueId.HasValue)
            {
                addToQueueRequest.SourceQueueId = sourceQueueId.Value;
            }

            addToQueueRequest.Target = entity.ToEntityReference();
            addToQueueRequest.DestinationQueueId = destinationQueueId;
            AddToQueueResponse _response = (AddToQueueResponse)this.service.Execute(addToQueueRequest);
            return _response.QueueItemId;
        }
    }
}
