using Microsoft.AspNetCore.Mvc;
using OrderAPI.Models;
using OrderAPI.Services;
using System.Collections.Generic;

namespace OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetOrder()
        {
            var orders = _orderService.GetAllOrders();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public ActionResult<Order> GetOrderByID(int id)
        {
            var order = _orderService.GetOrderByID(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        public ActionResult<Order> CreateOrder([FromBody] Order order)
        {
            if (order == null)
            {
                return BadRequest();
            }
            var createdOrder = _orderService.CreateOrder(order);
            return CreatedAtAction(nameof(GetOrderByID), new { id = createdOrder.Id }, createdOrder);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, [FromBody] Order order)
        {
            if (order == null || order.Id != id)
            {
                return BadRequest();
            }
            var existingOrder = _orderService.GetOrderByID(id);
            if (existingOrder == null)
            {
                return NotFound();
            }
            _orderService.UpdateOrder(order);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var existingOrder = _orderService.GetOrderByID(id);
            if (existingOrder == null)
            {
                return NotFound();
            }
            _orderService.DeleteOrder(id);
            return NoContent();
        }

    }
}
