using Domain.Contracts;
using Domain.Models.ProductModule;
using Microsoft.EntityFrameworkCore;
using Persentation.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DataSeed : IDataSeeding
    {
        private readonly StoreDbContext context;

        public DataSeed(StoreDbContext context)
        {
            this.context = context;
        }
      public async Task DataSeedDataAsync()
        {
            if ((await (context.Database.GetPendingMigrationsAsync())).Any())
            {await context.Database.MigrateAsync(); }
            if (!context.ProductTypes.Any())
            {
                var productTypes = File.OpenRead(@"G:\ITI\Module2\WebApi\Route\E_Commerce\Website\Persistence\Data\DataSeed\types.json");
                var types = await JsonSerializer.DeserializeAsync<List<ProductType>>(productTypes);
                if (types != null && types.Any())
                {
                    await context.AddRangeAsync(types);
                }
            }
            if (!context.ProductBrands.Any())
            {
                var productBrands = File.OpenRead(@"G:\ITI\Module2\WebApi\Route\E_Commerce\Website\Persistence\Data\DataSeed\brands.json");
                var brands =await JsonSerializer.DeserializeAsync<List<ProductBrand>>(productBrands);
                if (brands != null && brands.Any())
                {
                   await context.AddRangeAsync(brands);
                    context.SaveChanges();
                }
            }
            if (!context.Products.Any())
            {
                var products = File.ReadAllText(@"G:\ITI\Module2\WebApi\Route\E_Commerce\Website\Persistence\Data\DataSeed\products.json");
                var productList = JsonSerializer.Deserialize<List<Product>>(products);
                if (productList != null && productList.Any())
                {
                    context.Products.AddRange(productList);
                    context.SaveChanges();
                }
            }
           await context.SaveChangesAsync();
        }

    }

}
