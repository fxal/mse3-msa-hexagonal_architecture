using System;

namespace mse3_msa_hexagonal_architecture
{
    /// <summary>
    /// TODO
    /// </summary>
    public interface ITicketSellService 
    {
        Order ReserveTicket(User buyer, Ticket ticket);

        Order BuyTicket(Order order);
    
        Order CancelTicket(Order order);
    
    }
}