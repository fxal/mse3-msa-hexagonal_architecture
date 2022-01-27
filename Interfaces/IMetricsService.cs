namespace mse3_msa_hexagonal_architecture
{
    /// <summary>
    /// Handles publishing of metrics.
    /// </summary>
    public interface IMetricsService
    {
        /// <summary>
        /// Publishes metrics of an order
        /// </summary>
        /// <param name="order">The order</param>
        void PublishMetric(Order order);
    }
}