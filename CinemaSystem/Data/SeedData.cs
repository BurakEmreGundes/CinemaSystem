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




                var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                logger.LogInformation("Çekirdek veriler başarıyla yazıldı.");
            };
        }
    }
}
