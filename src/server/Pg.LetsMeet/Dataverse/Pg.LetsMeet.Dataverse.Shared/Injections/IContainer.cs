using System;

namespace Pg.LetsMeet.Dataverse.Shared.Injections
{
    public interface IContainer
    {
        void Register<TService, TConcrete>() where TService : class where TConcrete : class, TService;

        void Register<TService>(Func<TService> func) where TService : class;

        void RegisterInstance<TService>(TService obj) where TService : class;

        void RegisterSingleton<TService>(Func<TService> func) where TService : class;

        void RegisterSingleton<TService, TConcrete>() where TService : class where TConcrete : class, TService;

        void Verify();

        T GetInstance<T>() where T : class;

        object GetInstance(Type serviceType);
        bool AllowOverrides { get; set; }

        IExecutionScope GetCurrentExecutionScope();

        IExecutionScope BeginExecutionScope();
    }
}
