using System.ComponentModel.DataAnnotations;

namespace BakeryApi.Models;
public class Order
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = "Pending";

    // Foreign key for Customer
    public int CustomerId { get; set; }

    // Navigation property for Customer
    public Customer Customer { get; set; }

    // Navigation property for OrderItems
    public virtual List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
