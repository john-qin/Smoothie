using System.ComponentModel.DataAnnotations;

namespace Smoothie.Domain.ViewModels
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [StringLength(255, ErrorMessage = "Email must be 50 characters or fewer")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Your Email address is invalid")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        [StringLength(25, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 25 characters")]
        public string Password { get; set; }
    }
}
