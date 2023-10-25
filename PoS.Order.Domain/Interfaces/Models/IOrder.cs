namespace PoS.Order.Domain.Interfaces.Models
{
    /// <summary>
    /// Specifies any order-request's model prototype.
    /// </summary>
    public interface IOrder
    {
        /// <summary>
        /// The order-request's model unique identification alphanumeric combination number.
        /// </summary>
        public System.Guid Id { get; }
    }
}