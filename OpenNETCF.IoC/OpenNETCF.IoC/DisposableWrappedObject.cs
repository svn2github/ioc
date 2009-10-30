using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace OpenNETCF.IoC
{
    public class DisposableWrappedObject<T> : IDisposable
        where T : class, IDisposable
    {
        public bool Disposed { get; private set; }
        public T Instance { get; private set; }

        internal event EventHandler<GenericEventArgs<IDisposable>> Disposing;

        internal DisposableWrappedObject(T disposableObject)
        {
            if (disposableObject == null) throw new ArgumentNullException();

            Instance = disposableObject;
        }

        ~DisposableWrappedObject()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            lock(this)
            {
                if(Disposed) return;

                EventHandler<GenericEventArgs<IDisposable>> handler = Disposing;
                if(handler != null)
                {
                    Disposing(this, new GenericEventArgs<IDisposable>(Instance));
                }

                Instance.Dispose();

                Disposed = true;
            }
        }
    }
}
