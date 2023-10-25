using System;
using System.ComponentModel.DataAnnotations;
using PoS.Order.Domain.Settings;

namespace PoS.Order.Domain.Models
{
    /// <summary>
    /// Represents a general order request.
    /// </summary>
    public class DefaultOrderRequest : OrderType
    {
        /// <inheritdoc/>
        [Key]
        public override Guid Id { get; set; }

        /// <summary>
        /// The order-request's model service type.
        /// </summary>
        public OrderCategory Category { get; set; }

        /// <summary>
        /// The order-request Inquirer.
        /// </summary>
        public String Inquirer { get; set; }

        /// <summary>
        /// The order-request items.
        /// </summary>
        public String Instructions { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="category"></param>
        /// <param name="inquirer"></param>
        /// <param name="instructions"></param>
        public DefaultOrderRequest(Guid Id, OrderCategory category, String inquirer, String instructions)
        {
            this.Id = Id; this.Category = category; this.Inquirer = inquirer; this.Instructions = instructions;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="DefaultOrderRequest"/>.
        /// </summary>
        /// <param name="category">The order-request's model service type.</param>
        /// <param name="inquirer">The order-request Inquirer.</param>
        /// <param name="instructions">The order-request items.</param>
        public DefaultOrderRequest(OrderCategory category, String inquirer, String instructions)
        {
            this.Id = Guid.NewGuid(); this.Category = category; this.Inquirer = inquirer; this.Instructions = instructions;
        }

        /// <summary>
        /// Changes the <see cref="Category"/> of this current instance.
        /// </summary>
        /// <param name="category"></param>
        public void ChangeOrderType(OrderCategory category)
        {
            this.Id = Guid.NewGuid(); this.Category = category;
        }
    }
}