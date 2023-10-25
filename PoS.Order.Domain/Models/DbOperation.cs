using System;
using PoS.Order.Domain.Interfaces.Repository;
using PoS.Order.Domain.Settings;

namespace PoS.Order.Domain.Models
{
    /// <summary>
    /// Wrappes a database operation by providing a <see cref="DbOperationsStatus"/> status and the operation result around <typeparamref name="TEntity"/>, when instantiated in any properly targeted class.
    /// </summary>
    public class DbOperation<TEntity> : IDbOperation<TEntity> where TEntity : class
    {
#pragma warning disable CS8603
        private TEntity SecureInstance { get => this.Result; }
#pragma warning restore CS8603

        private TEntity? Entity { get; set; } = null;

        private DbOperationsStatus? Status { get; set; } = null;

        /// <inheritdoc/>
        public TEntity? Result
        {
            get
            {
                if (this.Entity is null)
                    throw new NullReferenceException("No entity is addressed to the result.");

                return this.Entity;
            }

            set { this.Entity = value; }
        }

        /// <inheritdoc/>
        public DbOperationsStatus? OperationStatus
        {
            get
            {
                if (this.Status is null)
                    throw new NullReferenceException("No entity is addressed to the result, therefore no status is set.");

                return this.Status;
            }

            set { this.Status = value; }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="DbOperation{TEntity}"/>.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="operationsStatus"></param>
        public DbOperation(TEntity? entity = null, DbOperationsStatus? operationsStatus = null)
        {
            this.Entity = entity;
            this.Status = operationsStatus;
        }

        /// <inheritdoc/>
        public Boolean HasOperationUnderlyingResult()
        {
            try { return this.Result is not null && this.Status is not null; } catch { return false; }
        }

        /// <inheritdoc/>
        public TEntity EnsureInstance()
        {
            if (this.HasOperationUnderlyingResult())
                return this.SecureInstance;

            throw new NullReferenceException("No entity is addressed to the result.");
        }
    }
}
