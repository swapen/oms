using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagementSystem.Models
{
    [Table("OrderDetails")]
    public class OrderDetails
    {
        [Required][Key]
        public int Id { get; set; }
        [Required]
        public string OrderDetailID { get; set; }
        [Required]
        public string SessionID { get; set; }
        [ForeignKey("ProductID")]
        public int ProductId { get; set; }
        public string UserName { get; set; }
        public int NumberOfUnits { get; set; }
        public int Price { get; set; }

        public virtual Products product { get; set; }

    }

}
