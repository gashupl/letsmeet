using Pg.LetsMeet.Azure.Core;
using Pg.LetsMeet.Dataverse.Context;
using Pg.LetsMeet.Events.Infrastructure.Repositories;
using Xunit;

namespace Pg.LetsMeet.Events.Infrastructure.Tests.Repositories
{
    public class AccountRepositoryTests : TestBase
    {

        [Fact]
        public void FindByAccountCode_CodeExists_ReturnAccount()
        {
            var service = this.FakeOrganizationService;
            service.Create(new Account()
            {
                AccountNumber = "1234"
            }); 
            var serviceFactory = new FakeOrganizationServiceFactory(service);
            var repo = new AccountRepository(serviceFactory);
            var account = repo.FindByAccountCode("1234");
            Assert.NotNull(account); 
        }

        [Fact]
        public void FindByAccountCode_MissingCode_ReturnNull()
        {
            var service = this.FakeOrganizationService;
            var serviceFactory = new FakeOrganizationServiceFactory(service);
            var repo = new AccountRepository(serviceFactory);
            var account = repo.FindByAccountCode("1234");
            Assert.Null(account);
        }
    }
}
