using OrderAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly List<Order> _orders = new List<Order>(); 
        public Order CreateOrder(Order order)
        {
            order.Id = _orders.Any() ? _orders.Max(o => o.Id) + 1 :1;
            _orders.Add(order);
            return order;
        }

        public void DeleteOrder(int id)
        {
            var order = GetOrderByID(id);
            if(order != null)
            {
                _orders.Remove(order);
            }
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _orders;
        }

        public Order GetOrderByID(int id)
        {
            return _orders.FirstOrDefault(o => o.Id == id);
        }

        public void UpdateOrder(Order order)
        {
            var existingOrder = GetOrderByID(order.Id);
            if (existingOrder != null)
            {
                existingOrder.Item = order.Item;
                existingOrder.Quantity = order.Quantity;
            }
        }
    }
}
