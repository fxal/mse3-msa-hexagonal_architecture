using System;

namespace mse3_msa_hexagonal_architecture
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISocialMediaService 
    {
        void PublishSellEvent(Order order);
    }
}