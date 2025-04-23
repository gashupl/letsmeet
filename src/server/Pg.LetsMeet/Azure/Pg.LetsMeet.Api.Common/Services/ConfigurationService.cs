namespace Pg.LetsMeet.Api.Common.Services
{
    public class ConfigurationService : IConfigurationService
    {
        public string? GetValue(string key)
        {
            return Environment.GetEnvironmentVariable(key);
        }
    }
}
