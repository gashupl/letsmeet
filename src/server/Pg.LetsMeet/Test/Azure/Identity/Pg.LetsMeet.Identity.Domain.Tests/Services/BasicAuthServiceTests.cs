using Moq;
using Pg.LetsMeet.Api.Common.Services;
using Pg.LetsMeet.Identity.Domain.Services;
using System.Text;

namespace Pg.LetsMeet.Identity.Domain.Tests.Services
{
    public class BasicAuthServiceTests
    {
        [Fact]
        public void IsValid_ValidHeader_ReturnTrue()
        {
            var expectedLogin = "admin";
            var expectedPassword = "P@ssw0rd1";

            var configServiceMock = new Mock<IConfigurationService>();
            configServiceMock.Setup(x => x.GetValue("b2cLogin")).Returns(expectedLogin);
            configServiceMock.Setup(x => x.GetValue("b2cPassword")).Returns(expectedPassword);

            string headerText = $"{expectedLogin}:{expectedPassword}";
            byte[] textBytes = Encoding.UTF8.GetBytes(headerText);
            string encodedHeader = $"Basic {Convert.ToBase64String(textBytes)}";

            var authService = new BasicAuthService(configServiceMock.Object);
            var actual = authService.IsValid(encodedHeader);
            
            Assert.True(actual);
        }

        [Fact]
        public void IsVald_InvalidHeader_ReturnFalse()
        {
            var expectedLogin = "admin";
            var expectedPassword = "P@ssw0rd1";

            var configServiceMock = new Mock<IConfigurationService>();
            configServiceMock.Setup(x => x.GetValue("b2cLogin")).Returns(expectedLogin);
            configServiceMock.Setup(x => x.GetValue("b2cPassword")).Returns(expectedPassword);

            string headerText = $"{expectedLogin}:P@ssw0rd2"; //Password different that the exepcted one
            byte[] textBytes = Encoding.UTF8.GetBytes(headerText);
            string encodedHeader = $"Basic {Convert.ToBase64String(textBytes)}";

            var authService = new BasicAuthService(configServiceMock.Object);
            var actual = authService.IsValid(encodedHeader);

            Assert.False(actual);
        }
    }
}
