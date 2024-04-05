using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
namespace employeeproject.Models
{
    public class ChangePw
    {
        [Key]
        public string Id {  get; set; }
        //public string UserId { get; set; }

        [Required(ErrorMessage = "Password is must be have 8 charater and atleast one symbol")]
        [MinLength(4, ErrorMessage = "Password is too short")]
        [Display(Name = "Old Password")]
        [RegularExpression(@"^(?=.*\d)(?=.*[\W_]).{4,}$", ErrorMessage = "Password must contain at least one digit and one special character.")]
        public string? OldPassword { get; set; }

        [Required(ErrorMessage = "Password is must be have 8 charater and atleast one symbol")]
        [MinLength(4, ErrorMessage = "Password is too short")]
        [Display(Name = "Confirm Password")]
        [RegularExpression(@"^(?=.*\d)(?=.*[\W_]).{4,}$", ErrorMessage = "Password must contain at least one digit and one special character.")]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Password is must be have 8 charater and atleast one symbol")]
        [MinLength(4, ErrorMessage = "Password is too short")]
        [Display(Name = "New Password")]
        [RegularExpression(@"^(?=.*\d)(?=.*[\W_]).{4,}$", ErrorMessage = "Password must contain at least one digit and one special character.")]
        public string? NewPassword { get; set; }
    }
}
