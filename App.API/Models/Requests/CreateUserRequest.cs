using System.ComponentModel.DataAnnotations;

namespace App.API.Models.Requests
{
    public class CreateUserRequest
    {
        [Required(ErrorMessage = "Username is required.")]
        [RegularExpression("^[a-zA-Z][a-zA-Z0-9._-]*$",
            ErrorMessage = "Username must start with a letter and can only contain letters, numbers, dots, hyphens, or underscores.")]
        [StringLength(30, MinimumLength = 3,
            ErrorMessage = "Username must be between 3 and 30 characters long.")]
        public string? UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
            ErrorMessage = "Email must be a valid format without spaces (e.g., name@example.com).")]
        [MaxLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        public string? Email { get; set; }
        
        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string? Password { get; set; }
    }
}