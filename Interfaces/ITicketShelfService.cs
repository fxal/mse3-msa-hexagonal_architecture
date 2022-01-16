using System;

namespace mse3_msa_hexagonal_architecture
{
    public interface ITicketShelfService 
    {
        //TODO
        Order PersistOrder(Order order);

        List<Order> FetchOrdersForTicket(Ticket ticket);

        User FetchSellerOfTicket(Ticket ticket);
    }
}