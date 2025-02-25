using BakeryApi.ViewModels;
namespace BakeryApi.Interfaces;
public interface ICustomerRepository
{
    Task<List<CustomersViewModel>> GetCustomersAsync();
    Task<CustomerViewModel> GetCustomerByIdAsync(int id);
    Task<bool> AddCustomerAsync(CustomerViewModel customer);
}
