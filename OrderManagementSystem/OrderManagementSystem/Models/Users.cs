using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagementSystem.Models
{
    [Table("Users")]
    public class Users
    {
        [Required][Key]
       // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress(ErrorMessage ="The email address provided is not a valid Email!")]
        public string EmailID { get; set; }
        public string Pword { get;  set; }
        [Compare("Pword", ErrorMessage ="The value entered does not match with the Password")]
        public string ConfirmPassword { get; set; }
        
    }
}
