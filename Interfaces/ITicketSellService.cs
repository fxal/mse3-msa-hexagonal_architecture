namespace mse3_msa_hexagonal_architecture
{
    /// <summary>
    /// Implements the ticket sell business logic
    /// </summary>
    public interface ITicketSellService 
    {
        /// <summary>
        /// Reserves a ticket as long there is no other existing order 
        /// in status reserved or pending regarding the ticket.
        /// Otherwise an exception is thrown, which is handled by inbound adapter later on.
        /// A new order is created by fetching the ticket seller through the ticketShelfService
        /// and setting the orderState to "Reserved".
        /// The reserved order will be persisted in database 
        /// using the ticketSehlfService.
        /// </summary>
        /// <param name="buyer">The buyer who wants to reserve the ticket</param>
        /// <param name="ticket">The ticket to be reserved</param>
        /// <returns>Returns the order object with orderstate "Reserved"</returns>
        Order ReserveTicket(User buyer, Ticket ticket);

        /// <summary>
        /// Locks the ticket for further reservations by setting the order state to "Pending".
        /// Calls the accountignservice for transferring the money. 
        /// When the accountinservice successfully decreased the buyer's balance 
        /// and increased the seller's balance, the order get the new state "Sold", otherwise the order is rolled back to "Reserved" state.
        /// The order is persisted using the ticketSehlfService.
        /// Additionally, when the transaction was successful, the successful selling metric is increased by publishing an event using the metricService
        /// and a social media posts are created by publishing an event using the socialMediaService.
        /// </summary>
        /// <param name="order">The order which should be fulfilled</param>
        /// <returns>The order in state "Sold" when transaction was successful or in state "Reserved" when transaction failed</returns>
        Order BuyTicket(Order order);

        /// <summary>
        /// Cancels an order.
        /// Only orders in state "Reserved" are allowed to be canceled.
        /// Using the ticketSelfService to persist the canceled order
        /// and increases the canceled order counter metric by publishing an event through the metric Service.
        /// </summary>
        /// <param name="order">The order to be canceled</param>
        /// <returns>The order in state "Cancelled" or any other state than "Reserved" </returns>    
        Order CancelTicket(Order order);
    
    }
}