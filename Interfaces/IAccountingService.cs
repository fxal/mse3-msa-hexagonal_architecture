namespace mse3_msa_hexagonal_architecture
{
    // TODO: sigrid
    public interface IAccountingService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        /// <returns></returns>
        bool TransferBalance(User origin, User target, float amount);
    }
}