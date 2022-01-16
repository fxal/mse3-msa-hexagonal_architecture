using System;

namespace mse3_msa_hexagonal_architecture
{
    // TODO: sigrid
    public interface IAccountingService 
    {
        bool TransferBalance(User origin, User target, float amount);
    }
}