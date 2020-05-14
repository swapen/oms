using System;
using Microsoft.AspNetCore.Http;

namespace OrderManagementSystem.Models
{
    public class ProductsViewModel
    {           
        public string ProductName { get; set; }
        public string Category { get; set; }
        public string Supplier { get; set; }
        public string ProductDescription { get; set; }
        public int AvailableUnits { get; set; }
        public int Price { get; set; }
        public IFormFile Image { get; set; }
    }
}
