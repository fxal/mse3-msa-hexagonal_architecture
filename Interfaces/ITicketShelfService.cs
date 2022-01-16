using System;
using System.Collections.Generic;

namespace mse3_msa_hexagonal_architecture
{
    /// <summary>
    /// Handles ticket and order related persistence.
    /// </summary>
    public interface ITicketShelfService 
    {
        /// <summary>
        /// Persists an order in its given configuration in the database.
        /// </summary>
        /// <param name="order">The order to persist</param>
        /// <returns>The persisted order as it was written to database</returns>
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

        /// <summary>
        /// Fetches the seller to the given ticket from database.
        /// </summary>
        /// <param name="ticket">The ticket for that the seller shall be fetched</param>
        /// <returns>The seller</returns>
        User FetchSellerOfTicket(Ticket ticket);
    }
}