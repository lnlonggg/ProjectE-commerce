using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyShop.Core.Entities;
using MyShop.Core.Helpers;
using System;
using System.Linq;

namespace MyShop.Core.Data
{
    public static class DataSeeder
    {
        public static void SeedAdmin(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var adminCfg = configuration.GetSection("AdminUser");

                var username = adminCfg["Username"];

                if (!db.Users.Any(u => u.Username == username))
                {
                    var user = new User
                    {
                        Id = Guid.NewGuid(),
                        Username = username!,
                        Email = adminCfg["Email"]!,
                        PasswordHash = PasswordHelper.Hash(adminCfg["Password"]!),
                        Role = Role.Admin,
                        CreatedAt = DateTime.UtcNow,
                        IsActive = true
                    };
                    db.Users.Add(user);
                    db.SaveChanges();
                }
            }
        }
    }
}