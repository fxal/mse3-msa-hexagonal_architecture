namespace mse3_msa_hexagonal_architecture
{
    /// <summary>
    /// TODO
    /// </summary>
    public interface ITicketSellService 
    {

        // TODO: Ermis

        /// <summary>method <c>ReserveTicket</c> ... t</summary>
        Order ReserveTicket(User buyer, Ticket ticket);

        Order BuyTicket(Order order);
    
        Order CancelTicket(Order order);
    
    }
}