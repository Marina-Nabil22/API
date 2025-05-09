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

        public async Task DataSeedAsync()
        {
            try
            {
                var PendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
                if (PendingMigrations.Any()) // Fixed typo here
                {
                    await _dbContext.Database.MigrateAsync();
                }


                if (!_dbContext.ProductBrands.Any())
                {
                    Console.WriteLine("Seeding ProductBrands...");
                    //  var ProductBrandData =await File.ReadAllTextAsync(@"C:\Users\ASUS\source\repos\E-Commerce.Web\Infrastructure\Persistence\Data\DataSeed\brands.json");
                    var ProductBrandData = File.OpenRead(@"C:\Users\ASUS\source\repos\E-Commerce.Web\Infrastructure\Persistence\Data\DataSeed\brands.json");

                    var ProductBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(ProductBrandData);
                    if (ProductBrands is not null && ProductBrands.Any())
                    {
                        await _dbContext.ProductBrands.AddRangeAsync(ProductBrands);
                    }
                }

                Console.WriteLine("Checking for ProductTypes...");
                if (!_dbContext.ProductTypes.Any())
                {
                    Console.WriteLine("Seeding ProductTypes...");
                    var ProductTypeData = File.OpenRead(@"C:\Users\ASUS\source\repos\E-Commerce.Web\Infrastructure\Persistence\Data\DataSeed\types.json");

                    var ProductTypes = await JsonSerializer.DeserializeAsync<List<ProductType>>(ProductTypeData);

                    if (ProductTypes is not null && ProductTypes.Any())
                    {
                        await _dbContext.ProductTypes.AddRangeAsync(ProductTypes);
                    }

                }
                Console.WriteLine("Checking for Products...");

                if (!_dbContext.Products.Any())
                {
                    Console.WriteLine("Seeding Products...");
                    var ProductData = File.OpenRead(@"C:\Users\ASUS\source\repos\E-Commerce.Web\Infrastructure\Persistence\Data\DataSeed\products.json");

                    var Products = await JsonSerializer.DeserializeAsync<List<Product>>(ProductData);

                    if (Products is not null && Products.Any())
                    {
                        await _dbContext.Products.AddRangeAsync(Products);
                    }

                }

                Console.WriteLine("Saving data to the database...");
                await _dbContext.SaveChangesAsync();
                Console.WriteLine("Database seeding completed.");


            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error during data seeding: {ex.Message}");
            }
        }
    }
}
