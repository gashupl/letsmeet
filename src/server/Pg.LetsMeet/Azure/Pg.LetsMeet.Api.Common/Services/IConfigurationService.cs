namespace Pg.LetsMeet.Api.Common.Services
{
    public interface IConfigurationService
    {
        string? GetValue(string key);
    }
}
