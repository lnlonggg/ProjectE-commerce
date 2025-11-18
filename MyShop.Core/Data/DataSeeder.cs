using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyShop.Core.Entities;
using MyShop.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyShop.Core.Data
{
    public static class DataSeeder
    {
        // Phương thức chính gọi tất cả các seed
        public static void SeedAll(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                // 1. Seed Admin (Giữ nguyên logic cũ)
                SeedAdmin(db, configuration);

                // 2. Seed Categories, Groups, Products (Mới)
                SeedCatalog(db);
            }
        }

        private static void SeedAdmin(AppDbContext db, IConfiguration configuration)
        {
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

        private static void SeedCatalog(AppDbContext db)
        {
            // Chỉ seed nếu chưa có Category nào
            if (db.Categories.Any()) return;

            // 1. Tạo Categories
            var categories = new List<Category>
            {
                new Category { Name = "Điện tử", OrderIndex = 1, CreatedAt = DateTime.UtcNow },
                new Category { Name = "Thời trang", OrderIndex = 2, CreatedAt = DateTime.UtcNow },
                new Category { Name = "Gia dụng", OrderIndex = 3, CreatedAt = DateTime.UtcNow }
            };
            db.Categories.AddRange(categories);
            db.SaveChanges(); // Lưu để lấy Id

            // 2. Tạo Groups
            var groups = new List<Group>
            {
                new Group { Name = "Điện thoại", CategoryId = categories[0].Id, Description = "Smartphones" },
                new Group { Name = "Laptop", CategoryId = categories[0].Id, Description = "Máy tính xách tay" },
                new Group { Name = "Áo nam", CategoryId = categories[1].Id, Description = "Áo phông, sơ mi" },
                new Group { Name = "Quần nữ", CategoryId = categories[1].Id, Description = "Quần jean, kaki" },
                new Group { Name = "Bếp", CategoryId = categories[2].Id, Description = "Dụng cụ nhà bếp" }
            };
            db.Groups.AddRange(groups);
            db.SaveChanges(); // Lưu để lấy Id

            // 3. Tạo 20 Products
            var products = new List<Product>();
            var random = new Random();

            for (int i = 1; i <= 20; i++)
            {
                // Chọn ngẫu nhiên 1 Group
                var randomGroup = groups[random.Next(groups.Count)];

                products.Add(new Product
                {
                    Name = $"Sản phẩm mẫu {i}",
                    Description = $"Mô tả cho sản phẩm {i} thuộc nhóm {randomGroup.Name}",
                    Price = random.Next(10, 500) * 10000, // Giá từ 100k đến 5tr
                    Stock = random.Next(5, 100),
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    GroupId = randomGroup.Id
                });
            }

            db.Products.AddRange(products);
            db.SaveChanges();
        }
    }
}