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

        /// <summary>method <c>ReserveTicket</c> ... </summary>
        public Order ReserveTicket(User buyer, Ticket ticket) {
            List<Order> persistedOrdersForTickets = ticketShelfService.FetchOrdersForTicket(ticket);
            bool isUnavailable = persistedOrdersForTickets.Exists(order => {
                if(order.OrderState == OrderState.Reserved 
                    || order.OrderState == OrderState.Pending) {
                    return true;
                }
                return false;
            });

            if(!isUnavailable) {
                throw new Exception("Ticket is already reserved");
            }

            Order order = new Order() {
                Ticket = ticket,
                Seller = ticketShelfService.FetchSellerOfTicket(ticket),
                Buyer = buyer,
                OrderState = OrderState.Reserved,
            };

            return ticketShelfService.PersistOrder(order);
        }

        public Order BuyTicket(Order order){
            order.OrderState = OrderState.Pending;
            order = ticketShelfService.PersistOrder(order);

            bool successfulTransaction = 
                accountingService.TransferBalance(order.Seller, order.Buyer, order.Ticket.TicketPrice);

            order.OrderState = successfulTransaction ? OrderState.Sold : OrderState.Reserved;

            Order completedOrder = ticketShelfService.PersistOrder(order);

            if (successfulTransaction) {
                metricsService.PublishMetric(order);
                socialMediaService.PublishSellEvent(order);
            }

            return completedOrder;
        }
    
        public Order CancelTicket(Order order){
            if ( order.OrderState == OrderState.Reserved) {
                order.OrderState = OrderState.Cancelled;

                Order cancelledOrder = ticketShelfService.PersistOrder(order);
                metricsService.PublishMetric(cancelledOrder);
                return cancelledOrder;
            }
            return order;
        }
    }
}