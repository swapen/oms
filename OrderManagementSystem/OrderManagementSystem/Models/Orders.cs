using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagementSystem.Models
{
    [Table("Orders")]
    public class Order    {
        [Key]
        [Required]
        public int OrderID { get; set; }
        public string SessionID { get; set; }
        //The value that binds Order table and OrderDetail table
        public string OrderDetailID { get; set; }
        [ForeignKey("UserId")]
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string ZipCode { get; set; }
        [Column("CurrentState")]
        public string State { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        //public decimal OrderTotal { get; set; }
        public decimal OrderTotal { get; set; }
        public DateTime TimeOfOrder { get; set; }


        public virtual Users user { get; set; }

    }
}
