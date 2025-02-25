using BakeryApi.Data;
using BakeryApi.Interfaces;
using BakeryApi.Models;
using BakeryApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BakeryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _repo;
        private readonly BakeryContext _context;

        public OrdersController(IOrderRepository repo, BakeryContext context)
        {
            _repo = repo;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders(int? orderNumber, string orderDate)
        {
            var query = _context.Orders.AsQueryable();

            if (orderNumber.HasValue)
            {
                query = query.Where(o => o.OrderId == orderNumber.Value);
            }

            if (!string.IsNullOrEmpty(orderDate))
            {
                var date = DateTime.Parse(orderDate);
                query = query.Where(o => o.OrderDate.Date == date.Date);
            }

            return Ok(await query.ToListAsync());
        }

        [HttpGet("customer/{id}")]
        public async Task<IActionResult> GetCustomerOrders(int id)
        {
            return Ok(await _repo.GetCustomerOrders(id));
        }

        [HttpGet("order/{id}")]
        public async Task<IActionResult> GetOrderDetails(int id)
        {
            return Ok(await _repo.GetOrderById(id));
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(OrderViewModel order)
        {
            try
            {
                if(await _repo.AddOrder(order))
                {
                    return StatusCode(201);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
