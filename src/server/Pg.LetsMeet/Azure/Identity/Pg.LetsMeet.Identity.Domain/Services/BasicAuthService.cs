using Pg.LetsMeet.Api.Common.Services;
using System.Text;

namespace Pg.LetsMeet.Identity.Domain.Services
{
    public class BasicAuthService : IAuthService
    {
        private readonly IConfigurationService _configurationService;
        public BasicAuthService(IConfigurationService configurationService)
        {
            _configurationService = configurationService;   
        }

        public bool IsValid(string authHeader)
        {
            string prefix = "Basic ";

            var expectedLogin = _configurationService.GetValue("b2cLogin");
            var expectedPassword = _configurationService.GetValue("b2cPassword");

            string decodedValue = String.Empty;

            if (authHeader != null && authHeader.StartsWith(prefix))
            {
                // Extract the Base64 encoded string
                int start = prefix.Length;
                int length = authHeader.Length - prefix.Length;
                string authHeaderSubstring = authHeader.Substring(start, length);
                byte[] decodedBytes = Convert.FromBase64String(authHeaderSubstring);
                decodedValue = Encoding.UTF8.GetString(decodedBytes);

                string expectedValue = $"{expectedLogin}:{expectedPassword}";
                //byte[] textBytes = Encoding.UTF8.GetBytes(expectedValue);
                //string expectedHeader = Convert.ToBase64String(textBytes);

                if (expectedValue == decodedValue)
                {
                    return true; 
                }
            }

            return false; 
        }
    }
}
