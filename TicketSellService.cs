using System;
using System.Collections.Generic;

namespace mse3_msa_hexagonal_architecture
{

    /// <inheritdoc/>
    public class TicketSellService : ITicketSellService
    {
        private ITicketShelfService ticketShelfService;

        private IAccountingService accountingService;

        private ISocialMediaService socialMediaService;

        private IMetricsService metricsService;

        /// <inheritdoc/>
        public Order ReserveTicket(User buyer, Ticket ticket)
        {
            List<Order> persistedOrdersForTicket = ticketShelfService.FetchOrdersForTicket(ticket);
            bool isUnavailable = persistedOrdersForTicket.Exists(order =>
                order.OrderState == OrderState.Reserved
                || order.OrderState == OrderState.Pending);

            if (isUnavailable)
            {
                throw new Exception("Ticket is already reserved");
            }

            Order order = new Order()
            {
                Ticket = ticket,
                Seller = ticketShelfService.FetchSellerOfTicket(ticket),
                Buyer = buyer,
                OrderState = OrderState.Reserved,
            };

            return ticketShelfService.PersistOrder(order);
        }

        /// <inheritdoc/>
        public Order BuyTicket(Order order)
        {
            order.OrderState = OrderState.Pending;
            Order pendingOrder = ticketShelfService.PersistOrder(order);

            bool successfulTransaction =
                accountingService.TransferBalance(pendingOrder.Seller, pendingOrder.Buyer, pendingOrder.Ticket.TicketPrice);

            pendingOrder.OrderState = successfulTransaction ? OrderState.Sold : OrderState.Reserved;

            Order completedOrder = ticketShelfService.PersistOrder(pendingOrder);

            if (successfulTransaction)
            {
                metricsService.PublishMetric(completedOrder);
                socialMediaService.PublishSellEvent(completedOrder);
            }

            return completedOrder;
        }

        /// <inheritdoc/>
        public Order CancelTicket(Order order)
        {
            if (order.OrderState == OrderState.Reserved)
            {
                order.OrderState = OrderState.Cancelled;

                Order cancelledOrder = ticketShelfService.PersistOrder(order);
                metricsService.PublishMetric(cancelledOrder);
                return cancelledOrder;
            }
            return order;
        }
    }
}