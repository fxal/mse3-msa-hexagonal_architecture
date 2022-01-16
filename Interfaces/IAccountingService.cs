using System;

namespace mse3_msa_hexagonal_architecture
{
    public interface IAccountingService 
    {
        bool TransferBalance(User origin, User target, float amount);
    }
}