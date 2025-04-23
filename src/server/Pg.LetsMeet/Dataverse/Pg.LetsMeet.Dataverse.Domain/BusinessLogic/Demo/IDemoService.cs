using System;

namespace Pg.LetsMeet.Dataverse.Domain.BusinessLogic.Demo
{
    public interface IDemoService : IService
    {
        string DoSomething(string name, Guid userId); 
    }
}
