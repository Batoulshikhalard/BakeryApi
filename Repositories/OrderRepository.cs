using BakeryApi.Data;
using BakeryApi.Interfaces;
using BakeryApi.Models;
using BakeryApi.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BakeryApi.Repositories;
public class OrderRepository : IOrderRepository
{
    private readonly BakeryContext _context;
    public OrderRepository(BakeryContext context)
    {
        _context = context;
    }

    public async Task<bool> AddOrder(OrderViewModel model)
    {
        try
        {
            var order = new Order
            {
                CustomerId = model.CustomerId,
                OrderDate = DateTime.UtcNow,
                Status = "Pending",
            };
            
            await _context.Orders.AddAsync(order);
            
            foreach(var o in model.Items)
            {
                var orderItem = new OrderItem
                {
                    Order = order,
                    Price = o.Price,
                    ProductId = o.ProductId,
                    Quantity = o.Quantity,
                };
                await _context.OrderItems.AddAsync(orderItem);
            }

            return await _context.SaveChangesAsync() > 0;
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<List<OrdersViewModel>> GetCustomerOrders(int customerId)
    {
        var orders = await _context.Orders.Where(x => x.CustomerId == customerId)
            .Select(x => new OrdersViewModel
            {
                CustomerId = x.CustomerId,
                OrderId = x.OrderId,
                OrderDate = x.OrderDate,
                Status = x.Status
            }).ToListAsync();

            return orders;
    }

    public async Task<OrderViewModel> GetOrderById(int orderId)
    {
        var order = await _context.Orders
            .Where(x => x.OrderId == orderId)
            .Include(x => x.OrderItems)
            .FirstOrDefaultAsync();

        if (order == null)
            throw new Exception("order not found");


        var view = new OrderViewModel
        {
            CustomerId = order.CustomerId, 
            OrderId = order.OrderId,
            Status = order.Status,
            OrderDate = order.OrderDate,
            Items = order.OrderItems.Select(x => new OrderItemViewModel
            {
                Price = x.Price,
                ProductId = x.ProductId,
                Quantity = x.Quantity,
            }).ToList(),
        };

        return view;
    }

    public async Task<IEnumerable<OrdersViewModel>> GetOrders()
    {
        return await _context.Orders.Select(x => new OrdersViewModel 
        { 
            CustomerId = x.CustomerId,
            OrderDate = x.OrderDate,
            OrderId = x.OrderId,
            Status = x.Status
        }).ToListAsync();
    }
}