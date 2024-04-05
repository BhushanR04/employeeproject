using System.ComponentModel.DataAnnotations;

namespace employeeproject.Models
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Username is required")]
        [Display(Name = "Enter User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
