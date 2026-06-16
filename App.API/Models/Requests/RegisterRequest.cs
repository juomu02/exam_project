using System.ComponentModel.DataAnnotations;

namespace App.API.Models.Requests
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
    }
}