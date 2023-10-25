using PoS.Order.Domain.Settings;

namespace PoS.Order.Domain.Interfaces.Repository
{
    /// <summary>
    /// Performs a data-base repository-centered operation by specifying the underlying operation targeted result.
    /// </summary>
    public interface IDbOperation<TEntity>
    {
        /// <summary>
        /// The operation result.
        /// </summary>
        TEntity? Result { get; set; }

        /// <summary>
        /// The data-base repository-centered operation execution status.
        /// </summary>
        DbOperationsStatus? OperationStatus { get; set; }

        /// <summary>
        /// Gets the underlying operation result as a safe instance of <typeparamref name="TEntity"/>; however if no result is found to this current operation then an exception is launched at runtime.
        /// </summary>
        /// <returns>The result of the operation as non-nullable instance.</returns>
        /// <exception cref="System.NullReferenceException"></exception>
        TEntity EnsureInstance();

        /// <summary>
        /// Evaluates whether theres is an operation underlying instance result and an non-null data-base repository-centered operation status.
        /// </summary>
        /// <returns><see langword="true"/> whether there is an operation underlying instance result and an non-null data-base repository-centered operation status; otherwise <see langword="false"/>.</returns>
        System.Boolean HasOperationUnderlyingResult();
    }
}