using System;

namespace mse3_msa_hexagonal_architecture
{
    public interface IMetricsService 
    {
        void PublishMetric(Order order);
    }
}