namespace BakeryApi.ViewModels;
public class OrdersViewModel
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public string? Status { get; set; } 
    public int CustomerId { get; set; }
}
public class OrderViewModel : OrdersViewModel
{
    public List<OrderItemViewModel> Items { get; set; } = [];
}