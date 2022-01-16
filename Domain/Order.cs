public class Order {
    public Ticket Ticket { get; set; }
    public OrderStates OrderState { get; set; }
    public User Seller { get; set; }
    public User Buyer { get; set; }
}