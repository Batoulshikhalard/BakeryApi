namespace BakeryApi.ViewModels;
public class AddressViewModel
{
    public int AddressId { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
    public string? EntityType { get; set; } 
    public int EntityId { get; set; } 
}
