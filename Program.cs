using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using employeeproject.Models;
using System.Configuration;
using System.Data;

namespace employeeproject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var connstr = builder.Configuration.GetConnectionString("employeeprojectContext");
            builder.Services.AddDbContext<employeeprojectContext>
                (options => options.UseSqlServer(connstr));
            builder.Services.AddIdentity<Useremp, IdentityRole>
                (
                    options =>
                    {
                        options.Password.RequiredLength = 6;
                        options.Password.RequireUppercase = false;
                        options.Password.RequireDigit = false;
                        options.User.RequireUniqueEmail = true;
                    }
                ).AddEntityFrameworkStores<employeeprojectContext>()
                .AddDefaultTokenProviders();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}