using Moq;
using Pg.LetsMeet.Dataverse.Domain.BusinessLogic.Contact;
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

            Assert.True(result);
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

            Assert.False(result);
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

            Assert.True(result);
        }
    }
}
