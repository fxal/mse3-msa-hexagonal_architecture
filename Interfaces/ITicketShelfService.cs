using System;
using System.Collections.Generic;

namespace mse3_msa_hexagonal_architecture
{
    public interface ITicketShelfService 
    {
        //TODO: felix
        Order PersistOrder(Order order);

        /// <summary>
        /// Fetches all orders that concern the given ticket.
        /// Since a ticket can be reserved and the reservation can be
        /// cancelled, multiple orders with different buyers can be present
        /// in the database.
        /// </summary>
        /// <param name="ticket">The ticket</param>
        /// <returns>A list of orders concerning the given ticket</returns>
        List<Order> FetchOrdersForTicket(Ticket ticket);

        User FetchSellerOfTicket(Ticket ticket);
    }
}