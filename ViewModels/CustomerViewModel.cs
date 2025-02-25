using BakeryApi.Models;

namespace BakeryApi.ViewModels;
public class CustomersViewModel
{
    public int CustomerId { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
}
public class CustomerViewModel : CustomersViewModel
{
    public List<AddressViewModel> Addresses { get; set; } = [];
}