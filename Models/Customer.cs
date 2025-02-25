namespace BakeryApi.Models;
public class Customer
{
    public int CustomerId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    // Navigation property for addresses
    public List<Address> Addresses { get; set; } = new List<Address>();

    // Navigation property for orders
    public List<Order> Orders { get; set; } = new List<Order>();
}