using PoS.Order.Domain.Settings;

namespace PoS.Order.Domain.Interfaces.Models
{
    /// <summary>
    /// Specifies any order-request abstract model properties.
    /// </summary>
    public interface IOrderRequestInputModel
    {
        /// <summary>
        /// The order-request category.
        /// </summary>
        public OrderCategory Category { get; set; }

        /// <summary>
        /// The order-request Inquirer.
        /// </summary>
        System.String Inquirer { get; set; }

        /// <summary>
        /// The order-request items.
        /// </summary>
        System.String Instructions { get; set; }
    }
}