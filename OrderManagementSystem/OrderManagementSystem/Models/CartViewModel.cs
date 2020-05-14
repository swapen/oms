using System;
using Microsoft.AspNetCore.Http;

namespace OrderManagementSystem.Models
{
    public class CartViewModel
    {
        public ShoppingCart ShoppingCart { get; set; }
        public decimal ShoppingCartTotal { get; set; }

    }
}
