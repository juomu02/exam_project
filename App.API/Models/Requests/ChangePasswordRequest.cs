using System.ComponentModel.DataAnnotations;

namespace App.API.Models.Requests
{
    public class ChangePasswordRequest
    {
        [Required(ErrorMessage = "User ID is required.")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Current password is required.")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "New password is required.")]
        public string NewPassword { get; set; }
    }
}