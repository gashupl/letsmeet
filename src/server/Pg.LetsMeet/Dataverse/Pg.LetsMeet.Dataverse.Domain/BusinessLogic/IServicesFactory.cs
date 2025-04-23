namespace Pg.LetsMeet.Dataverse.Domain
{
    public interface IServicesFactory
    {
        T Get<T>() where T : class; 
    }
}
