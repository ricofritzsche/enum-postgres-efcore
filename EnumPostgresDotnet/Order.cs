namespace EnumPostgresDotnet;

public enum OrderStatus
{
    Pending,
    Processing,
    Completed,
    Cancelled
}

public class Order
{
    public int Id { get; init; }
    public OrderStatus Status { get; set; }
}