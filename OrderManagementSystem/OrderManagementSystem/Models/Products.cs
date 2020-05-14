using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagementSystem.Models
{
    [Table("Products")]
    public class Products
    {
        [Required][Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public string Supplier { get; set; }
        public string ProductDescription { get; set; }
        public int AvailableUnits { get; set; }
        public int Price { get; set; }
        public string ImageData { get; set; }
    }
}
