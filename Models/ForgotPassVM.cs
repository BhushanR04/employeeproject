using System.ComponentModel.DataAnnotations;

namespace employeeproject.Models
{
    public class ForgotPassVM
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Id { get; set; }
        public string Email { get; set; }
    }
}
