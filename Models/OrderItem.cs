using System.ComponentModel.DataAnnotations;

namespace BakeryApi.Models;
public class OrderItem
{
    public int OrderItemId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    // Foreign key for Order
    public int OrderId { get; set; }

    // Navigation property for Order
    public virtual Order Order { get; set; }

    // Foreign key for Product
    public int ProductId { get; set; }

    // Navigation property for Product
    public virtual Product Product { get; set; }
}

