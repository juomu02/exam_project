using System.ComponentModel.DataAnnotations;

namespace App.API.Models.Requests
{
    public class ChangeEmailRequest
    {        
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
            ErrorMessage = "Email must be a valid format without spaces (e.g., name@example.com).")]
        [MaxLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        public string Email { get; set; }
    }
}