using System;
using System.Runtime.ConstrainedExecution;

namespace LCL
{
    public abstract class DisposableObject : CriticalFinalizerObject, IDisposable
    {
        ~DisposableObject()
        {
            this.Dispose(false);
        }
        protected abstract void Dispose(bool disposing);
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
     internal sealed class NullDisposable : IDisposable
    {
        public static NullDisposable Instance  = new NullDisposable();

        private NullDisposable()
        {
            
        }

        public void Dispose()
        {

        }
    }
}
