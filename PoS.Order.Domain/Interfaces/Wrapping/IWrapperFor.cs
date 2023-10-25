using System;

namespace PoS.Order.Domain.Interfaces.Wrapping
{
    /// <summary>
    /// Specifies the default bahavior and properties that any living artifact <see cref="IWrapperFor{WrappedType}"/> implementation must have by encapsulating any <typeparamref name="WrappedType"/>.
    /// </summary>
    /// <typeparam name="WrappedType">The artifcat to be wrapped around.</typeparam>
    public interface IWrapperFor<WrappedType>
    {
        /// <summary>
        /// The wrapped artifact.
        /// </summary>
        /// <exception cref="NullReferenceException"></exception>
        public WrappedType ResultValue { get; init; }

        /// <summary>
        /// Indicates whether there's a any wrapped result or not.
        /// </summary>
        /// <returns><see langword="true"/> whether there's any non-null value attached on the result, otherwise <see langword="false"/>.</returns>
        public Boolean HasWrappedResultValue();
    }
}