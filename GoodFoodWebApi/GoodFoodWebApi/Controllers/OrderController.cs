using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DataAccess;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private GoodFoodDbContext _context;

        public OrderController(GoodFoodDbContext goodFoodDbContext)
        {
            _context = goodFoodDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetOrders([FromQuery] int orderAmount)
        {
            List<Order> orders = new List<Order>();
            try
            {
                for (int i = 0; i < orderAmount; i++)
                {
                    orders.Add(_context.Orders.FirstOrDefault(order => order.OrderId == _context.Orders.Count()-1-i));
                }

                return Ok(orders);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder([FromBody] Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var orderWithProperId = _context.Orders.Add(order).Entity;
                await _context.SaveChangesAsync();
                return Created($"/{orderWithProperId.OrderId}", orderWithProperId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}