using System;

namespace LCL.Repositories
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Gets a <see cref="System.Boolean"/> value which indicates
        /// whether the Unit of Work could support Microsoft Distributed
        /// Transaction Coordinator (MS-DTC).
        /// </summary>
        bool DistributedTransactionSupported { get; }
        /// <summary>
        /// Gets a <see cref="System.Boolean"/> value which indicates
        /// whether the Unit of Work was successfully committed.
        /// </summary>
        bool Committed { get; }
        /// <summary>
        /// Commits the transaction.
        /// </summary>
        void Commit();
        /// <summary>
        /// Rollback the transaction.
        /// </summary>
        void Rollback();
    }
}