using System;
using PoS.Order.Domain.Interfaces.Wrapping;

namespace PoS.Order.Domain.Wrappers
{
    /// <summary>
    /// Wrappes any living artifcat and exposes access for it.
    /// </summary>
    /// <typeparam name="WrappedType">The artifact type.</typeparam>
    public class WrapperFor<WrappedType> : IWrapperFor<WrappedType>
    {
        private WrappedType WrappedArtifact { get; set; }

        /// <inheritdoc/>
        public WrappedType ResultValue { get => this.WrappedArtifact; init { this.WrappedArtifact = value; } }

        /// <summary>
        /// Initializes a new instance of <see cref="WrapperFor{WrappedType}"/> with the specified artifact.
        /// </summary>
        /// <param name="artifact">The artifcat to be wrapped around.</param>
        public WrapperFor(WrappedType artifact) => this.WrappedArtifact = artifact;

        /// <inheritdoc/>
        public Boolean HasWrappedResultValue() => this.ResultValue is not null;
    }
}