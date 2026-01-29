using FakeXrmEasy;
using Microsoft.Xrm.Sdk;
using Pg.LetsMeet.Dataverse.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Pg.LetsMeet.Dataverse.Infrastructure.Tests
{
    public class ContactRepositoryTests
    {
        [Fact]
        public void GetParentCustomerRef_ContactExists_ReturnsParentCustomerRef()
        {
            var contactId = Guid.NewGuid(); 
            var expectedCustomerRef = new EntityReference(Account.EntityLogicalName, Guid.NewGuid());

            var context = new XrmFakedContext();
            context.ProxyTypesAssembly = Assembly.GetAssembly(typeof(pg_eventparticipation));

            context.Initialize(new List<Entity>() {
                new Contact(){ Id = contactId, ParentCustomerId = expectedCustomerRef
                },
            });

            var service = context.GetOrganizationService();

            var repo = new ContactRepository();
            repo.Initialize(service);
            var actualCustomerRef = repo.GetParentCustomerRef(contactId);

            Assert.NotNull(actualCustomerRef); 
            Assert.Equal(expectedCustomerRef.Id, actualCustomerRef.Id);
        }

        [Fact]
        public void GetParentCustomerRef_ContactNotExists_ReturnsNull()
        {
            var contactId = Guid.NewGuid();

            var context = new XrmFakedContext();
            context.ProxyTypesAssembly = Assembly.GetAssembly(typeof(pg_eventparticipation));

            var service = context.GetOrganizationService();

            var repo = new ContactRepository();
            repo.Initialize(service);
            var actualCustomerRef = repo.GetParentCustomerRef(contactId);

            Assert.Null(actualCustomerRef);
        }

        [Fact]
        public void GetParentCustomerRef_MissingParentCustomer_ReturnsNull()
        {
            var contactId = Guid.NewGuid();

            var context = new XrmFakedContext();
            context.ProxyTypesAssembly = Assembly.GetAssembly(typeof(pg_eventparticipation));

            context.Initialize(new List<Entity>() {
                new Contact(){ Id = contactId
                },
            });

            var service = context.GetOrganizationService();

            var repo = new ContactRepository();
            repo.Initialize(service);
            var actualCustomerRef = repo.GetParentCustomerRef(contactId);

            Assert.Null(actualCustomerRef);
        }

        [Fact]
        public void GetContactByEmail_ContactExists_ReturnsContact()
        {
            var contactId = Guid.NewGuid();
            var email = "test@example.com";

            var context = new XrmFakedContext();
            context.ProxyTypesAssembly = Assembly.GetAssembly(typeof(pg_eventparticipation));

            context.Initialize(new List<Entity>() {
                new Contact(){ Id = contactId, EMailAddress1 = email
                },
            });

            var service = context.GetOrganizationService();

            var repo = new ContactRepository();
            repo.Initialize(service);
            var actualContact = repo.GetContactByEmail(email);

            Assert.NotNull(actualContact);
            Assert.Equal(contactId, actualContact.Id);
        }

        [Fact]
        public void GetContactByEmail_ContactNotExists_ReturnsNull()
        {
            var contactId = Guid.NewGuid();
            var email = "test@example.com";

            var context = new XrmFakedContext();
            context.ProxyTypesAssembly = Assembly.GetAssembly(typeof(pg_eventparticipation));

            var service = context.GetOrganizationService();

            var repo = new ContactRepository();
            repo.Initialize(service);
            var actualContact = repo.GetContactByEmail(email);

            Assert.Null(actualContact);
        }
    }
}
