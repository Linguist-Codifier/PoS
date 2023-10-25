namespace PoS.Order.Domain.Settings
{
    /// <summary>
    /// Specifies the order-request's model execution scope.
    /// </summary>
    public enum OrderCategory : System.UInt16
    {
        /// <summary>
        /// Specifies that the order-request'a model should be carried to the <see cref="Fries"/> execution queue.
        /// </summary>
        Fries = 0,

        /// <summary>
        /// Specifies that the order-request'a model should be carried to the <see cref="Grill"/> execution queue.
        /// </summary>
        Grill,

        /// <summary>
        /// Specifies that the order-request'a model should be carried to the <see cref="Salad"/> execution queue.
        /// </summary>
        Salad,

        /// <summary>
        /// Specifies that the order-request'a model should be carried to the <see cref="Drink"/> execution queue.
        /// </summary>
        Drink,

        /// <summary>
        /// Specifies that the order-request'a model should be carried to the <see cref="Desert"/> execution queue.
        /// </summary>
        Desert
    }
}
