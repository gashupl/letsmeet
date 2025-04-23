using SimpleInjector.Lifestyles;
using System;

namespace Pg.LetsMeet.Dataverse.Shared.Injections
{
    public class Container : IContainer, IDisposable
    {
        private readonly SimpleInjector.Container container;

        public Container()
        {
            // Initialize container
            container = new SimpleInjector.Container();

            // Setup default behavior
            container.Options.DefaultScopedLifestyle = new ThreadScopedLifestyle();//When CRM supports .Net 4.6.0 use AsyncScopedLifestyle
            container.Options.DefaultLifestyle = SimpleInjector.Lifestyle.Scoped;
            container.Options.PropertySelectionBehavior = new DependencyInjectionPropertySelectionBehavior();

            // Register the container with itself
            container.RegisterInstance<IContainer>(this);
        }

        public T GetInstance<T>() where T : class
        {
            return container.GetInstance<T>();
        }

        public object GetInstance(Type serviceType)
        {
            return container.GetInstance(serviceType);
        }

        public IExecutionScope GetCurrentExecutionScope()
        {
            var scope = SimpleInjector.Lifestyle.Scoped.GetCurrentScope(this.container);
            return scope.GetItem(scope) as IExecutionScope;
        }

        public IExecutionScope BeginExecutionScope()
        {
            var scope = ThreadScopedLifestyle.BeginScope(container);
            var exScope = new ExecutionScope(scope);
            return exScope;
        }

        public void Register<TService, TConcrete>() where TService : class where TConcrete : class, TService
        {
            container.Register<TService, TConcrete>();
        }

        public void Register<TService>(Func<TService> func) where TService : class
        {
            container.Register<TService>(func, SimpleInjector.Lifestyle.Scoped);
        }

        public void Verify()// verify and diagnose this container
        {
            container.Verify();
        }

        public void RegisterSingleton<TService, TConcrete>() where TService : class where TConcrete : class, TService // register auto-wireup singleton
        {
            container.RegisterSingleton<TService, TConcrete>();
        }

        public void RegisterInstance<TService>(TService obj) where TService : class // register already created instance object
        {
            container.RegisterInstance<TService>(obj);
        }

        public void RegisterSingleton<TService>(Func<TService> func) where TService : class // register delegate factory method to create singleton object (Register factory delegates)
        {
            container.RegisterSingleton<TService>(func);
        }

        public T GetItem<T>(object key) where T : class
        {
            // Resolve the current connectionScope for the IoC container
            var scope = SimpleInjector.Lifestyle.Scoped.GetCurrentScope(this.container);

            // Retrieve the item from the current connectionScope (or directly from the container, if we are not inside a connectionScope)
            return scope != null ? scope.GetItem(key) as T : this.container.ContainerScope.GetItem(key) as T;
        }

        public void SetItem(object key, object item)
        {
            // Resolve the current connectionScope for the IoC container
            var scope = SimpleInjector.Lifestyle.Scoped.GetCurrentScope(this.container);

            // Set the item on the current connectionScope (or directly from the container, if we are not inside a connectionScope)
            if (scope != null) scope.SetItem(key, item);
            else container.ContainerScope.SetItem(key, item);
        }

        public bool AllowOverrides
        {
            get => container.Options.AllowOverridingRegistrations;
            set => container.Options.AllowOverridingRegistrations = value;
        }

        public IExecutionScope BeginScope() => new ExecutionScope(AsyncScopedLifestyle.BeginScope(this.container));

        public virtual bool IsDisposed { get; protected set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    container.Dispose();
                }
                IsDisposed = true;
            }
        }
    }
}
