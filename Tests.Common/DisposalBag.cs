using System;
using System.Collections.Generic;

namespace Tests.Common
{
    public class DisposalBag : IDisposable
    {
        private List<IDisposable> _disposables;

        private List<IDisposable> Disposables => _disposables ??= new List<IDisposable>();


        public void Add(IDisposable disposable)
        {
            this.Disposables.Add(disposable);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this._disposables != null)
                {
                    var disposables = this._disposables;
                    this._disposables = null;
                    foreach (var disposable in disposables)
                    {
                        SafeDispose(disposable);
                    }
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private static void SafeDispose(IDisposable item)
        {
            try
            {
                item?.Dispose();
            }
            catch
            {
                // suppress exceptions to avoid problems
            }
        }
    }
}