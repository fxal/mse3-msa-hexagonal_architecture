namespace mse3_msa_hexagonal_architecture
{
    /// <summary>
    /// Handles events for posting on social media platforms.
    /// </summary>
    public interface ISocialMediaService
    {
        /// <summary>
        /// Publishes a event when an order is sold, so it can be shown on social media
        /// </summary>
        /// <param name="order">The order</param>
        void PublishSellEvent(Order order);
    }
}