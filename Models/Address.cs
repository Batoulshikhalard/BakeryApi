namespace BakeryApi.Models;
public class Address
{
    public int AddressId { get; set; }
    
    public string Street { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }

    public string EntityType { get; set; } // 'Customer' or 'Supplier'
    public int EntityId { get; set; } // CustomerId or SupplierId
}
