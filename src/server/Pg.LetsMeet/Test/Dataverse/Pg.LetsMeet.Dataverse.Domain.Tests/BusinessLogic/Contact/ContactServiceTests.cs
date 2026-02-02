using Microsoft.Xrm.Sdk;
using Moq;
using Pg.LetsMeet.Dataverse.Context;
using Pg.LetsMeet.Dataverse.Domain.BusinessLogic.Contacts;
using Pg.LetsMeet.Dataverse.Domain.DataAccess;
using Pg.LetsMeet.Dataverse.Tests.Shared;
using Xunit;

namespace Pg.LetsMeet.Dataverse.Domain.Tests.BusinessLogic
{
    public class ContactServiceTests : DataverseTestBase
    {
        [Fact]
        public void ContactExists_ContactFound_ReturnsTrue()
        {
            var email = "test@example.com";
            var contact = new Context.Contact
            {
                Id = System.Guid.NewGuid(),
                EMailAddress1 = email
            };

            var repo = new Mock<IContactRepository>();
            repo.Setup(r => r.GetContactByEmail(email)).Returns(contact);

            var repositoriesFactory = new Mock<IRepositoriesFactory>();
            repositoriesFactory.Setup(s => s.Get<IContactRepository>(null)).Returns(repo.Object);

            var service = new ContactService(repositoriesFactory.Object, CreateTracingService());
            var result = service.ContactExists(email);

            Assert.True(result.Exists);
            Assert.NotNull(result.Contact);
        }

        [Fact]
        public void ContactExists_ContactNotFound_ReturnsFalse()
        {
            var email = "nonexistent@example.com";

            var repo = new Mock<IContactRepository>();
            repo.Setup(r => r.GetContactByEmail(email)).Returns((Context.Contact)null);

            var repositoriesFactory = new Mock<IRepositoriesFactory>();
            repositoriesFactory.Setup(s => s.Get<IContactRepository>(null)).Returns(repo.Object);

            var service = new ContactService(repositoriesFactory.Object, CreateTracingService());
            var result = service.ContactExists(email);

            Assert.False(result.Exists);
        }

        [Fact]
        public void ContactExists_CaseInsensitiveEmail_ReturnsTrue()
        {
            var email = "Test@Example.Com";
            var contact = new Context.Contact
            {
                Id = System.Guid.NewGuid(),
                EMailAddress1 = "test@example.com"
            };

            var repo = new Mock<IContactRepository>();
            repo.Setup(r => r.GetContactByEmail(email)).Returns(contact);

            var repositoriesFactory = new Mock<IRepositoriesFactory>();
            repositoriesFactory.Setup(s => s.Get<IContactRepository>(null)).Returns(repo.Object);

            var service = new ContactService(repositoriesFactory.Object, CreateTracingService());
            var result = service.ContactExists(email);

            Assert.True(result.Exists);
        }

        [Fact]
        public void UpdateContactIfChanged_BothFieldsChanged_UpdatesContactAndReturnsTrue()
        {
            var contact = new Context.Contact
            {
                Id = System.Guid.NewGuid(),
                FirstName = "OldFirstName",
                LastName = "OldLastName"
            };

            var repo = new Mock<IContactRepository>();
            repo.Setup(r => r.Update(It.IsAny<Entity>()));

            var repositoriesFactory = new Mock<IRepositoriesFactory>();
            repositoriesFactory.Setup(s => s.Get<IContactRepository>(null)).Returns(repo.Object);

            var service = new ContactService(repositoriesFactory.Object, CreateTracingService());
            var result = service.UpdateContactIfChanged(contact, "NewFirstName", "NewLastName");

            Assert.True(result);
            Assert.Equal("NewFirstName", contact.FirstName);
            Assert.Equal("NewLastName", contact.LastName);
            repo.Verify(r => r.Update(contact), Times.Once);
        }

        [Fact]
        public void UpdateContactIfChanged_NoFieldsChanged_DoesNotUpdateAndReturnsFalse()
        {
            var contact = new Context.Contact
            {
                Id = System.Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe"
            };

            var repo = new Mock<IContactRepository>();
            repo.Setup(r => r.Update(It.IsAny<Entity>()));

            var repositoriesFactory = new Mock<IRepositoriesFactory>();
            repositoriesFactory.Setup(s => s.Get<IContactRepository>(null)).Returns(repo.Object);

            var service = new ContactService(repositoriesFactory.Object, CreateTracingService());
            var result = service.UpdateContactIfChanged(contact, "John", "Doe");

            Assert.False(result);
            Assert.Equal("John", contact.FirstName);
            Assert.Equal("Doe", contact.LastName);
            repo.Verify(r => r.Update(It.IsAny<Entity>()), Times.Never);
        }

        [Fact]
        public void UpdateContactIfChanged_OnlyFirstNameChanged_UpdatesContactAndReturnsTrue()
        {
            var contact = new Context.Contact
            {
                Id = System.Guid.NewGuid(),
                FirstName = "OldFirstName",
                LastName = "Doe"
            };

            var repo = new Mock<IContactRepository>();
            repo.Setup(r => r.Update(It.IsAny<Entity>()));

            var repositoriesFactory = new Mock<IRepositoriesFactory>();
            repositoriesFactory.Setup(s => s.Get<IContactRepository>(null)).Returns(repo.Object);

            var service = new ContactService(repositoriesFactory.Object, CreateTracingService());
            var result = service.UpdateContactIfChanged(contact, "NewFirstName", "Doe");

            Assert.True(result);
            Assert.Equal("NewFirstName", contact.FirstName);
            Assert.Equal("Doe", contact.LastName);
            repo.Verify(r => r.Update(contact), Times.Once);
        }

        [Fact]
        public void UpdateContactIfChanged_OnlyLastNameChanged_UpdatesContactAndReturnsTrue()
        {
            var contact = new Context.Contact
            {
                Id = System.Guid.NewGuid(),
                FirstName = "John",
                LastName = "OldLastName"
            };

            var repo = new Mock<IContactRepository>();
            repo.Setup(r => r.Update(It.IsAny<Entity>()));

            var repositoriesFactory = new Mock<IRepositoriesFactory>();
            repositoriesFactory.Setup(s => s.Get<IContactRepository>(null)).Returns(repo.Object);

            var service = new ContactService(repositoriesFactory.Object, CreateTracingService());
            var result = service.UpdateContactIfChanged(contact, "John", "NewLastName");

            Assert.True(result);
            Assert.Equal("John", contact.FirstName);
            Assert.Equal("NewLastName", contact.LastName);
            repo.Verify(r => r.Update(contact), Times.Once);
        }
    }
}
