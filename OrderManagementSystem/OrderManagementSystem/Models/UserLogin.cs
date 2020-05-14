using System;
using System.ComponentModel.DataAnnotations;

namespace OrderManagementSystem.Models
{
    public class UserLogin
    {
        public UserLogin()
        {
        }
        [Required]
        [EmailAddress(ErrorMessage = "The email address provided is not a valid Email!")]
        public string EmailID { get; set; }
        [Required]
        public string Pword { get; set; }
        //[Display(Name = "Remember Me")]
        //public bool RememberMe { get; set; }
    }
}
