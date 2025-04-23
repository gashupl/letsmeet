using System;

namespace Pg.LetsMeet.Dataverse.Shared.Injections
{
    public interface IExecutionScope : IDisposable
    {

        T GetItem<T>(object key) where T : class;

        T GetItem<T>() where T : class;

        void SetItem(object key, object item, bool dispose = false);

         void SetItem<T>(T item, bool disposeWhenScopeEnds = false) where T : class;

        void RegisterForDisposal(IDisposable disposable);
    }
}
