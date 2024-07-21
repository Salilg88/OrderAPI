using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public int Quantity { get; set; }
    }
}
