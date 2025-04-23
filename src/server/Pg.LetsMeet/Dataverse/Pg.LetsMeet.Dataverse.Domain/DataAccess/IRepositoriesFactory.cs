using System;

namespace Pg.LetsMeet.Dataverse.Domain.DataAccess
{
    public interface IRepositoriesFactory
    {
        T Get<T>(Guid? userId = null) where T : IRepository; 
    }
}