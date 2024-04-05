using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient.DataClassification;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace employeeproject.Models
{
    public class Useremp:IdentityUser
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Key]
        //public int UserId { get; set; }

        [Required(ErrorMessage = "User type is compulsary")]
        public string? Utype { get; set; }

        //public string? Username { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of birth")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DOB { get; set; }


        [Required(ErrorMessage = "Enter M for male and F for female")]
        public string? Gender { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Joing date of Employees")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime HiredDate { get; set; }

        [Required(ErrorMessage = "Enter the department of employees")]
        public string? Department { get; set; }

        [DataType(DataType.Currency)]
        [Range(1000, 500000, ErrorMessage = "salary too high or low")]
        public double Salary { get; set; }



    }
}
