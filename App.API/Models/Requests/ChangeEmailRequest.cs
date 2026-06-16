using System.ComponentModel.DataAnnotations;

namespace App.API.Models.Requests
{
    public class ChangeEmailRequest
    {
        [Required(ErrorMessage = "User ID is required.")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "New email is required.")]
        public string Email { get; set; }
    }
}