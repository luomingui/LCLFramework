using System;
using System.Threading;

namespace LCL.Threading
{
    /// <summary>
    /// 一个拥有方便使用的API的读写锁。
    /// </summary>
    public class SimpleRWLock : IDisposable
    {
        private ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        /// <summary>
        /// 开始读操作，请使用using语法。
        /// </summary>
        /// <returns></returns>
        public IDisposable BeginRead( )
        {
            this._lock.EnterReadLock();

            return new ReadDisposor()
            {
                Owner = this._lock
            };
        }

        /// <summary>
        /// 开始写操作，请使用using语法。
        /// </summary>
        /// <returns></returns>
        public IDisposable BeginWrite( )
        {
            this._lock.EnterWriteLock();

            return new WriteDisposor()
            {
                Owner = this._lock
            };
        }

        private class ReadDisposor : IDisposable
        {
            public ReaderWriterLockSlim Owner;

            public void Dispose( )
            {
                this.Owner.ExitReadLock();
            }
        }

        private class WriteDisposor : IDisposable
        {
            public ReaderWriterLockSlim Owner;

            public void Dispose( )
            {
                this.Owner.ExitWriteLock();
            }
        }

        public void Dispose( )
        {
            this._lock.Dispose();
        }
    }

    public class WriteLockDisposable : IDisposable
    {
        private readonly ReaderWriterLockSlim _rwLock;
        public WriteLockDisposable(ReaderWriterLockSlim rwLock)
        {
            _rwLock = rwLock;
            _rwLock.EnterWriteLock();
        }

        void IDisposable.Dispose()
        {
            _rwLock.ExitWriteLock();
        }
    }
}
