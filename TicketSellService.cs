using System;
using System.Collections.Generic;

namespace mse3_msa_hexagonal_architecture
{
 
    public class TicketSellService : ITicketSellService
    {
        private ITicketShelfService ticketShelfService;

        private IAccountingService accountingService;

        private ISocialMediaService socialMediaService;

        private IMetricsService metricsService;

        /// <inheritdoc/>
        public Order ReserveTicket(User buyer, Ticket ticket) {
            List<Order> persistedOrdersForTickets = ticketShelfService.FetchOrdersForTicket(ticket);
            bool isUnavailable = persistedOrdersForTickets.Exists(order => 
                order.OrderState == OrderStates.Reserved 
                || order.OrderState == OrderStates.Pending);

            if(!isUnavailable) {
                throw new Exception("Ticket is already reserved");
            }

            Order order = new Order() {
                Ticket = ticket,
                Seller = ticketShelfService.FetchSellerOfTicket(ticket),
                Buyer = buyer,
                OrderState = OrderStates.Reserved,
            };

            return ticketShelfService.PersistOrder(order);
        }

        /// <inheritdoc/>
        public Order BuyTicket(Order order){
            order.OrderState = OrderStates.Pending;
            order = ticketShelfService.PersistOrder(order);

            bool successfulTransaction = 
                accountingService.TransferBalance(order.Seller, order.Buyer, order.Ticket.TicketPrice);

            order.OrderState = successfulTransaction ? OrderStates.Sold : OrderStates.Reserved;

            Order completedOrder = ticketShelfService.PersistOrder(order);

            if (successfulTransaction) {
                metricsService.PublishMetric(order);
                socialMediaService.PublishSellEvent(order);
            }

            return completedOrder;
        }

        /// <inheritdoc/>
        public Order CancelTicket(Order order){
            if ( order.OrderState == OrderStates.Reserved) {
                order.OrderState = OrderStates.Cancelled;

                Order cancelledOrder = ticketShelfService.PersistOrder(order);
                metricsService.PublishMetric(cancelledOrder);
                return cancelledOrder;
            }
            return order;
        }
    }
}