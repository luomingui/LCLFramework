
using System.Collections.Generic;

namespace LCL.Bus.DirectBus
{
    /// <summary>
    /// Represents the message bus that will dispatch the messages immediately once
    /// the bus is committed.
    /// </summary>
    public abstract class DirectBus : DisposableObject, IBus
    {
        #region Private Fields
        private volatile bool committed = true;
        private readonly IMessageDispatcher dispatcher;
        private readonly Queue<object> messageQueue = new Queue<object>();
        private readonly object queueLock = new object();
        private object[] backupMessageArray;
        #endregion

        #region Ctor
        /// <summary>
        /// Initializes a new instance of <c>DirectBus&lt;TMessage&gt;</c> class.
        /// </summary>
        /// <param name="dispatcher">The <see cref="Apworks.Bus.IMessageDispatcher"/> which
        /// dispatches messages in the bus.</param>
        public DirectBus(IMessageDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="disposing">A <see cref="System.Boolean"/> value which indicates whether
        /// the object should be disposed explicitly.</param>
        protected override void Dispose(bool disposing)
        {
        }
        #endregion

        #region IBus Members
        /// <summary>
        /// Publishes the specified message to the bus.
        /// </summary>
        /// <param name="message">The message to be published.</param>
        public void Publish<TMessage>(TMessage message)
        {
            lock (queueLock)
            {
                messageQueue.Enqueue(message);
                committed = false;
            }
        }
        /// <summary>
        /// Publishes a collection of messages to the bus.
        /// </summary>
        /// <param name="messages">The messages to be published.</param>
        public void Publish<TMessage>(IEnumerable<TMessage> messages)
        {
            lock (queueLock)
            {
                foreach (var message in messages)
                {
                    messageQueue.Enqueue(message);
                }
                committed = false;
            }
        }
        /// <summary>
        /// Clears the published messages waiting for commit.
        /// </summary>
        public void Clear()
        {
            lock (queueLock)
            {
                this.messageQueue.Clear();
            }
        }
        #endregion

        #region IUnitOfWork Members
        /// <summary>
        /// Gets a <see cref="System.Boolean"/> value which indicates
        /// whether the Unit of Work could support Microsoft Distributed
        /// Transaction Coordinator (MS-DTC).
        /// </summary>
        public bool DistributedTransactionSupported
        {
            get { return false; }
        }
        /// <summary>
        /// Gets a <see cref="System.Boolean"/> value which indicates
        /// whether the Unit of Work was successfully committed.
        /// </summary>
        public bool Committed
        {
            get { return this.committed; }
        }
        /// <summary>
        /// Commits the transaction.
        /// </summary>
        public void Commit()
        {
            lock (queueLock)
            {
                backupMessageArray = new object[messageQueue.Count];
                messageQueue.CopyTo(backupMessageArray, 0);
                while (messageQueue.Count > 0)
                {
                    dispatcher.DispatchMessage(messageQueue.Dequeue());
                }
                committed = true;
            }
        }
        /// <summary>
        /// Rollback the transaction.
        /// </summary>
        public void Rollback()
        {
            lock (queueLock)
            {
                if (backupMessageArray != null && backupMessageArray.Length > 0)
                {
                    messageQueue.Clear();
                    foreach (var msg in backupMessageArray)
                    {
                        messageQueue.Enqueue(msg);
                    }
                }
                committed = false;
            }
        }
        #endregion
    }
}
