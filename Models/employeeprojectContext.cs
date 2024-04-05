using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Odbc;
using employeeproject.Models;

namespace employeeproject.Models
{
    public class employeeprojectContext : IdentityDbContext<Useremp, IdentityRole, string>
    {
        public employeeprojectContext(DbContextOptions<employeeprojectContext> options)
        : base(options)
        {
        }
        public DbSet<Useremp> Users {  get; set; }
        public DbSet<employeeproject.Models.ForgotPassVM>? ForgotPassVM { get; set; }

    }
}