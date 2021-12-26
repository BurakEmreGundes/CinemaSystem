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
                var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                var userManager = serviceProvider.GetRequiredService<UserManager<Customer>>();


                var normalUser = new Customer { UserName = "normal@gmail.com", Email = "normal@gmail.com", EmailConfirmed = true };
                await userManager.CreateAsync(normalUser, "123");

                var student = new Customer { UserName = "b191210013@sakarya.edu.tr", Email = "b191210013@sakarya.edu.tr",EmailConfirmed = true };
                await userManager.CreateAsync(student, "123");



                var roleManager = serviceProvider.GetRequiredService<RoleManager<CustomerRole>>();
                await roleManager.CreateAsync(new CustomerRole { Name = "Admin" });
                await roleManager.CreateAsync(new CustomerRole { Name = "NormalUser" });

                /*  await context.Genders.AddAsync(new Gender { GenderType="Kadın"});
                  await context.Genders.AddAsync(new Gender { GenderType = "Erkek" });
                  await context.Genders.AddAsync(new Gender { GenderType = "Diğer" });

                  await context.Categories.AddAsync(new Category { CategoryName="Komedi" });
                  await context.Categories.AddAsync(new Category { CategoryName="Bilim Kurgu" });

                  await context.Provinces.AddAsync(new Province {ProvinceName="İstanbul" });

                  await context.Districts.AddAsync(new District{ Id=34,ProvinceId=1 });
                  await context.SaveChangesAsync();
                 */



                logger.LogInformation("Çekirdek veriler başarıyla yazıldı.");
            };
        }
    }
}
