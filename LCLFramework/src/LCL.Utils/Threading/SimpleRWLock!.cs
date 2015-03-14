using System;
using System.Collections;
using System.Threading;

namespace LCL.Threading
{
    /// <summary>
    /// 提供并发读写控制的实用方法
    /// </summary>
    public static class ConcurrencyUtils
    {
        /// <summary>
        /// 该方法确保读的时候不会有人写
        /// </summary>
        public static void AtomRead(System.Action action)
        {
            new ReaderWriterLockSlim().AtomRead(action);
        }
        /// <summary>
        /// 该方法确保读的时候不会有人写
        /// </summary>
        public static void AtomRead(this ReaderWriterLockSlim readerWriterLockSlim, System.Action action)
        {
            if (readerWriterLockSlim == null)
            {
                throw new ArgumentNullException("readerWriterLockSlim");
            }
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            readerWriterLockSlim.EnterReadLock();

            try
            {
                action();
            }
            finally
            {
                readerWriterLockSlim.ExitReadLock();
            }
        }
        /// <summary>
        /// 该方法确保读的时候不会有人写
        /// </summary>
        public static T AtomRead<T>(Func<T> function)
        {
            return new ReaderWriterLockSlim().AtomRead<T>(function);
        }
        /// <summary>
        /// 该方法确保读的时候不会有人写
        /// </summary>
        public static T AtomRead<T>(this ReaderWriterLockSlim readerWriterLockSlim, Func<T> function)
        {
            if (readerWriterLockSlim == null)
            {
                throw new ArgumentNullException("readerWriterLockSlim");
            }
            if (function == null)
            {
                throw new ArgumentNullException("function");
            }

            readerWriterLockSlim.EnterReadLock();

            try
            {
                return function();
            }
            finally
            {
                readerWriterLockSlim.ExitReadLock();
            }
        }
        /// <summary>
        /// 该方法确保读任意时刻只有一个人写
        /// </summary>
        public static void AtomWrite(System.Action action)
        {
            new ReaderWriterLockSlim().AtomWrite(action);
        }
        /// <summary>
        /// 该方法确保读任意时刻只有一个人写
        /// </summary>
        public static void AtomWrite(this ReaderWriterLockSlim readerWriterLockSlim, System.Action action)
        {
            if (readerWriterLockSlim == null)
            {
                throw new ArgumentNullException("readerWriterLockSlim");
            }
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            readerWriterLockSlim.EnterWriteLock();

            try
            {
                action();
            }
            finally
            {
                readerWriterLockSlim.ExitWriteLock();
            }
        }
        /// <summary>
        /// 该方法确保读任意时刻只有一个人写
        /// </summary>
        public static T AtomWrite<T>(Func<T> function)
        {
            return new ReaderWriterLockSlim().AtomWrite<T>(function);
        }
        /// <summary>
        /// 该方法确保读任意时刻只有一个人写
        /// </summary>
        public static T AtomWrite<T>(this ReaderWriterLockSlim readerWriterLockSlim, Func<T> function)
        {
            if (readerWriterLockSlim == null)
            {
                throw new ArgumentNullException("readerWriterLockSlim");
            }
            if (function == null)
            {
                throw new ArgumentNullException("function");
            }

            readerWriterLockSlim.EnterWriteLock();

            try
            {
                return function();
            }
            finally
            {
                readerWriterLockSlim.ExitWriteLock();
            }
        }
    }
}