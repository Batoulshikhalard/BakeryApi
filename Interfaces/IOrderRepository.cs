using BakeryApi.ViewModels;

namespace BakeryApi.Interfaces;
public interface IOrderRepository
{
    Task<IEnumerable<OrdersViewModel>> GetOrders();
    Task<OrderViewModel> GetOrderById(int orderId);
    Task<bool> AddOrder(OrderViewModel order);
    Task<List<OrdersViewModel>> GetCustomerOrders(int customerId);
}
