using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagementSystem.Models
{
    [Table("ShoppingCartItems")]
    public class ShoppingCartItem
    {
        [Required][Key]
        [Column("ShoppingCartItemID")]
        public int ShoppingCartItemId { get; set; }
        [Column("ProductID")] [ForeignKey("ProductID")]
        public int ProductId { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public int NumberOfUnits { get; set; }
        //public string ShoppingCartId { get; set; }
        public int Price { get; set; }
    
        public virtual Products product { get; set; }
       
        public virtual Users user { get; set; }

    }
}
