using CinemaSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaSystem.Data
{
    public static class SeedData
    {
        public async static void Initialize(IServiceProvider serviceProvider)
        {

            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {

                var userManager = serviceProvider.GetRequiredService<UserManager<Customer>>();

                var admin = new Customer { UserName = "admin@gmail.com", Email = "admin@gmail.com", EmailConfirmed = true };
                await userManager.CreateAsync(admin, "135713579Bg-");

                var normalUser = new Customer { UserName = "normal@gmail.com", Email = "normal@gmail.com", EmailConfirmed = true };
                await userManager.CreateAsync(normalUser, "135713579Bg-");


                var roleManager = serviceProvider.GetRequiredService<RoleManager<CustomerRole>>();
                await roleManager.CreateAsync(new CustomerRole { Name = "Admin" });
                await roleManager.CreateAsync(new CustomerRole { Name = "NormalUser" });

                await userManager.AddToRoleAsync(admin, "Admin");
                await userManager.AddToRoleAsync(normalUser, "NormalUser");






                var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                logger.LogInformation("Çekirdek veriler başarıyla yazıldı.");
            };
        }
    }
}
