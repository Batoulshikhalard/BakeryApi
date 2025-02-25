namespace BakeryApi.Models;
public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; }

    public double Price { get; set; }

    // Navigation property for OrderItems
    public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

}
