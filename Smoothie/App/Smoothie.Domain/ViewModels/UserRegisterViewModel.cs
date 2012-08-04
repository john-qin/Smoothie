using System.ComponentModel.DataAnnotations;

namespace Smoothie.Domain.ViewModels
{
    public class UserRegisterViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [StringLength(255, ErrorMessage = "Email must be 50 characters or fewer")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Your Email address is invalid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Display name is required")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Display name must be between 2 and 25 characters")]
        public string DisplayName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        [StringLength(25, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 25 characters")]
        public string Password { get; set; }
    }
}
