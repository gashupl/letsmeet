using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pg.LetsMeet.Dataverse.Shared.Injections
{
    public class ExecutionScope : IExecutionScope, IDisposable
    {
        private readonly SimpleInjector.Scope scope;


        #region Constructor(s)

        public ExecutionScope(SimpleInjector.Scope scope)
        {
            Contract.Requires(scope != null);

            // Set local state
            this.scope = scope;
            scope.SetItem(scope, this);
        }

        #endregion

        #region Disposal method(s)

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
                    scope?.Dispose();
                }

                IsDisposed = true;
            }
        }

        #endregion

        public T GetItem<T>(object key) where T : class
        {
            return scope.GetItem(key) as T;
        }

        public T GetItem<T>() where T : class
        {
            return scope.Container.GetInstance<T>();
        }

        public void SetItem(object key, object item, bool dispose = false)
        {
            scope.SetItem(key, item);
            if (dispose && item is IDisposable)
                RegisterForDisposal(item as IDisposable);
        }

        public void SetItem<T>(T item, bool disposeWhenScopeEnds = false) where T : class
        {
            scope.Container.Register<T>(() => { return item; }, SimpleInjector.Lifestyle.Scoped);
            if (disposeWhenScopeEnds && item is IDisposable)
                RegisterForDisposal(item as IDisposable);
        }

        public void RegisterForDisposal(IDisposable disposable)
        {
            scope.RegisterForDisposal(disposable);
        }
    }
}
