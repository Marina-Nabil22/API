using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DataSeeding : IDataSeeding
    {
        private readonly StoreDbContext _dbContext;

        public DataSeeding(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void DataSeed()
        {
            try
            {
                if (_dbContext.Database.GetPendingMigrations().Any()) // Fixed typo here
                {
                    _dbContext.Database.Migrate();
                }

                Console.WriteLine("Checking for ProductBrands...");

                if (!_dbContext.ProductBrands.Any())
                {
                    Console.WriteLine("Seeding ProductBrands...");
                    var ProductBrandData = File.ReadAllText(@"C:\Users\ASUS\source\repos\E-Commerce.Web\Infrastructure\Persistence\Data\DataSeed\brands.json");
                    var ProductBrands = JsonSerializer.Deserialize<List<ProductBrand>>(ProductBrandData);
                    if (ProductBrands is not null && ProductBrands.Any())
                    {
                        _dbContext.ProductBrands.AddRange(ProductBrands);
                    }
                }

                Console.WriteLine("Checking for ProductTypes...");
                if (!_dbContext.ProductTypes.Any())
                {
                    Console.WriteLine("Seeding ProductTypes...");
                    var ProductTypeData = File.ReadAllText(@"C:\Users\ASUS\source\repos\E-Commerce.Web\Infrastructure\Persistence\Data\DataSeed\types.json");

                    var ProductTypes = JsonSerializer.Deserialize<List<ProductType>>(ProductTypeData);

                    if (ProductTypes is not null && ProductTypes.Any())
                    {
                        _dbContext.ProductTypes.AddRange(ProductTypes);
                    }

                }
                Console.WriteLine("Checking for Products...");

                if (!_dbContext.Products.Any())
                {
                    Console.WriteLine("Seeding Products...");
                    var ProductData = File.ReadAllText(@"C:\Users\ASUS\source\repos\E-Commerce.Web\Infrastructure\Persistence\Data\DataSeed\products.json");

                    var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);

                    if (Products is not null && Products.Any())
                    {
                        _dbContext.Products.AddRange(Products);
                    }

                }

                Console.WriteLine("Saving data to the database...");
                _dbContext.SaveChanges();
                Console.WriteLine("Database seeding completed.");


            }
            catch (Exception ex) {

                Console.WriteLine($"Error during data seeding: {ex.Message}");
            }

        }

     
    }
}
