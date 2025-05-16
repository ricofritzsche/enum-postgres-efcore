namespace EnumPostgresDotnet;

class Program
{
    static void Main(string[] args)
    {
        using var context = new AppDbContext();
        context.Database.EnsureCreated();

        // Create and save a new order
        var order = new Order { Status = OrderStatus.Processing };
        context.Orders.Add(order);
        context.SaveChanges();
        Console.WriteLine($"Saved Order Id={order.Id} with Status={order.Status}");

        // Query back
        var orders = context.Orders.ToList();
        Console.WriteLine("All orders in database:");
        foreach (var o in orders)
        {
            Console.WriteLine($"- Order Id={o.Id}, Status={o.Status}");
        }
    }
}