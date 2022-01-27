namespace mse3_msa_hexagonal_architecture
{
    /// <summary>
    /// Handles money transfers.
    /// </summary>
    public interface IAccountingService
    {
        /// <summary>
        /// Transfers an amount of money from an origin to a target (e.g. buyer to a seller).
        /// </summary>
        /// <param name="origin">The seller</param>
        /// <param name="target">The buyer</param>
        /// <param name="amount">Amount of money</param>
        /// <returns>True, if the money was transferred successfully, otherwise false.</returns>
        bool TransferBalance(User origin, User target, float amount);
    }
}