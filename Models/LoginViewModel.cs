using System.ComponentModel.DataAnnotations;

namespace LMVC.Models;

public class LoginViewModel
{
        [Required(ErrorMessage = "Please enter your email.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public required string Username { get; set; }

        [Required(ErrorMessage = "Please enter your password.")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
}