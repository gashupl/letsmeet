namespace Pg.LetsMeet.Dataverse.Domain.DataAccess
{
    public interface IEnvVariablesRepository : IRepository
    {
        string GetDefaultValue(string name); 
    }
}
