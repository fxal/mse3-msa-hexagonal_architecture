public class Order {
    public string OrderId { get; set; }
    public Ticket Ticket { get; set; }
    public OrderState OrderState { get; set; }
    public User Seller { get; set; }
    public User Buyer { get; set; }
}