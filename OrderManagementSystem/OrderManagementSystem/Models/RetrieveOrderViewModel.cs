using System;
using System.Collections.Generic;

namespace OrderManagementSystem.Models
{
    public class RetrieveOrderViewModel
    {
        public List<Order> order { get; set; }
        public List<OrderDetails> orderDetails { get; set; }

    }
}
