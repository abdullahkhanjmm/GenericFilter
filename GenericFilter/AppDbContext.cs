using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace GenericFilter
{
    public class AppDbContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
           new Category { Id = 1, Name = "Electronics" },
           new Category { Id = 2, Name = "Books" },
           new Category { Id = 3, Name = "Clothing" },
           new Category { Id = 4, Name = "Home Appliances" },
           new Category { Id = 5, Name = "Toys" },
           new Category { Id = 6, Name = "Sports" },
           new Category { Id = 7, Name = "Beauty" },
           new Category { Id = 8, Name = "Automotive" },
           new Category { Id = 9, Name = "Garden" },
           new Category { Id = 10, Name = "Office Supplies" }
       );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                // Electronics
                new Product { Id = 1, Name = "Smartphone", Price = 699.99m, CategoryId = 1 },
                new Product { Id = 2, Name = "Laptop", Price = 999.99m, CategoryId = 1 },
                new Product { Id = 3, Name = "Headphones", Price = 199.99m, CategoryId = 1 },
                new Product { Id = 4, Name = "Smartwatch", Price = 249.99m, CategoryId = 1 },
                new Product { Id = 5, Name = "Bluetooth Speaker", Price = 89.99m, CategoryId = 1 },

                // Books
                new Product { Id = 6, Name = "E-Book Reader", Price = 129.99m, CategoryId = 2 },
                new Product { Id = 7, Name = "Science Fiction Novel", Price = 19.99m, CategoryId = 2 },
                new Product { Id = 8, Name = "Cookbook", Price = 24.99m, CategoryId = 2 },
                new Product { Id = 9, Name = "Self-Help Book", Price = 14.99m, CategoryId = 2 },
                new Product { Id = 10, Name = "Historical Fiction", Price = 21.99m, CategoryId = 2 },

                // Clothing
                new Product { Id = 11, Name = "T-Shirt", Price = 15.99m, CategoryId = 3 },
                new Product { Id = 12, Name = "Jeans", Price = 39.99m, CategoryId = 3 },
                new Product { Id = 13, Name = "Jacket", Price = 89.99m, CategoryId = 3 },
                new Product { Id = 14, Name = "Sweater", Price = 49.99m, CategoryId = 3 },
                new Product { Id = 15, Name = "Dress", Price = 59.99m, CategoryId = 3 },

                // Home Appliances
                new Product { Id = 16, Name = "Air Conditioner", Price = 299.99m, CategoryId = 4 },
                new Product { Id = 17, Name = "Microwave Oven", Price = 89.99m, CategoryId = 4 },
                new Product { Id = 18, Name = "Washing Machine", Price = 499.99m, CategoryId = 4 },
                new Product { Id = 19, Name = "Refrigerator", Price = 799.99m, CategoryId = 4 },
                new Product { Id = 20, Name = "Dishwasher", Price = 399.99m, CategoryId = 4 },

                // Toys
                new Product { Id = 21, Name = "Building Blocks", Price = 29.99m, CategoryId = 5 },
                new Product { Id = 22, Name = "Dollhouse", Price = 89.99m, CategoryId = 5 },
                new Product { Id = 23, Name = "Action Figures", Price = 19.99m, CategoryId = 5 },
                new Product { Id = 24, Name = "Toy Car", Price = 14.99m, CategoryId = 5 },
                new Product { Id = 25, Name = "Board Game", Price = 24.99m, CategoryId = 5 },

                // Sports
                new Product { Id = 26, Name = "Basketball", Price = 29.99m, CategoryId = 6 },
                new Product { Id = 27, Name = "Tennis Racket", Price = 79.99m, CategoryId = 6 },
                new Product { Id = 28, Name = "Soccer Ball", Price = 22.99m, CategoryId = 6 },
                new Product { Id = 29, Name = "Golf Club", Price = 119.99m, CategoryId = 6 },
                new Product { Id = 30, Name = "Yoga Mat", Price = 39.99m, CategoryId = 6 },

                // Beauty
                new Product { Id = 31, Name = "Face Cream", Price = 29.99m, CategoryId = 7 },
                new Product { Id = 32, Name = "Shampoo", Price = 12.99m, CategoryId = 7 },
                new Product { Id = 33, Name = "Lipstick", Price = 19.99m, CategoryId = 7 },
                new Product { Id = 34, Name = "Perfume", Price = 49.99m, CategoryId = 7 },
                new Product { Id = 35, Name = "Nail Polish", Price = 9.99m, CategoryId = 7 },

                // Automotive
                new Product { Id = 36, Name = "Car Battery", Price = 129.99m, CategoryId = 8 },
                new Product { Id = 37, Name = "Engine Oil", Price = 29.99m, CategoryId = 8 },
                new Product { Id = 38, Name = "Tire", Price = 89.99m, CategoryId = 8 },
                new Product { Id = 39, Name = "Car Wash Kit", Price = 24.99m, CategoryId = 8 },
                new Product { Id = 40, Name = "GPS Navigation System", Price = 149.99m, CategoryId = 8 },

                // Garden
                new Product { Id = 41, Name = "Garden Tools Set", Price = 49.99m, CategoryId = 9 },
                new Product { Id = 42, Name = "Flower Pots", Price = 19.99m, CategoryId = 9 },
                new Product { Id = 43, Name = "Lawn Mower", Price = 299.99m, CategoryId = 9 },
                new Product { Id = 44, Name = "Outdoor Furniture", Price = 199.99m, CategoryId = 9 },
                new Product { Id = 45, Name = "Watering Can", Price = 14.99m, CategoryId = 9 },

                // Office Supplies
                new Product { Id = 46, Name = "Desk Chair", Price = 89.99m, CategoryId = 10 },
                new Product { Id = 47, Name = "Office Desk", Price = 199.99m, CategoryId = 10 },
                new Product { Id = 48, Name = "Printer", Price = 149.99m, CategoryId = 10 },
                new Product { Id = 49, Name = "Notebooks", Price = 9.99m, CategoryId = 10 },
                new Product { Id = 50, Name = "Desk Lamp", Price = 29.99m, CategoryId = 10 }
            );
        }
    }
}
