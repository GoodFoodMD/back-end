using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DataAccess;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [EnableCors("MyPolicy")]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private GoodFoodDbContext _context;

        public OrderController(GoodFoodDbContext goodFoodDbContext)
        {
            _context = goodFoodDbContext;
        }

        [HttpGet]
        [Route("/api/getOrder")]
        public async Task<ActionResult<List<Order>>> GetOrders([FromQuery] int orderAmount)
        {
            List<Order> orders = new List<Order>();
            try
            {
                for (int i = 0; i < orderAmount; i++)
                {
                    orders.Add(_context.Orders.FirstOrDefault(order => order.OrderId == _context.Orders.Count()-i));
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
        [Route("/api/postOrder")]
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