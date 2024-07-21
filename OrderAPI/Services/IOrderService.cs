using OrderAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderAPI.Services
{
    public interface IOrderService
    {
        IEnumerable<Order> GetAllOrders();
        Order GetOrderByID(int id);
        Order CreateOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(int id);



    }
}
