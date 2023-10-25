using System;
using System.ComponentModel.DataAnnotations;
using PoS.Order.Domain.Interfaces.Models;
using PoS.Order.Domain.Settings;

namespace PoS.Order.Domain.Models
{
    /// <summary>
    /// Specifies a wrapping order-request model.
    /// </summary>
    public class DefaultOrderRequestInputModel : IOrderRequestInputModel
    {
        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false, ErrorMessage = $"{nameof(this.Category)} is a required property.")]
        public OrderCategory Category { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false, ErrorMessage = $"{nameof(this.Inquirer)} is a required property.")]
        [MaxLength(100)]
        public String Inquirer { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false, ErrorMessage = $"{nameof(this.Instructions)} is a required property.")]
        [MaxLength(500)]
        public String Instructions { get; set; }

        /// <summary>
        /// Intialiazes a new instance of <see cref="DefaultOrderRequestInputModel"/> with the specified arguments.
        /// </summary>
        /// <param name="category">The order-request category.</param>
        /// <param name="inquirer">The order-request inquirer.</param>
        /// <param name="instructions">The order-request items.</param>
        public DefaultOrderRequestInputModel(OrderCategory category, String inquirer, String instructions)
        {
            this.Category = category; this.Inquirer = inquirer; this.Instructions = instructions;
        }
    }
}