using Domain.Contracts;
using Domain.Models.IdentityModule;
using Domain.Models.ProductModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persentation.Data;
using Persistence.Identity;
using System.Text.Json;


namespace Persistence
{
    public class DataSeed : IDataSeeding
    {
        private readonly StoreDbContext _dbcontext;
        private readonly IdentityStoreDbContext _identityStoreDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DataSeed(StoreDbContext context, IdentityStoreDbContext identityStoreDbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _dbcontext = context;
            _identityStoreDbContext = identityStoreDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task DataSeedDataAsync()
        {
            if ((await (_dbcontext.Database.GetPendingMigrationsAsync())).Any())
            { await _dbcontext.Database.MigrateAsync(); }
            if (!_dbcontext.ProductTypes.Any())
            {
                var productTypes = File.OpenRead(@"G:\ITI\Module2\WebApi\Route\E_Commerce\Website\Persistence\Data\DataSeed\types.json");
                var types = await JsonSerializer.DeserializeAsync<List<ProductType>>(productTypes);
                if (types != null && types.Any())
                {
                    await _dbcontext.AddRangeAsync(types);
                }
            }
            if (!_dbcontext.ProductBrands.Any())
            {
                var productBrands = File.OpenRead(@"G:\ITI\Module2\WebApi\Route\E_Commerce\Website\Persistence\Data\DataSeed\brands.json");
                var brands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(productBrands);
                if (brands != null && brands.Any())
                {
                    await _dbcontext.AddRangeAsync(brands);

                }
            }
            if (!_dbcontext.Products.Any())
            {
                var products = File.ReadAllText(@"G:\ITI\Module2\WebApi\Route\E_Commerce\Website\Persistence\Data\DataSeed\products.json");
                var productList = JsonSerializer.Deserialize<List<Product>>(products);
                if (productList != null && productList.Any())
                {
                    _dbcontext.Products.AddRange(productList);

                }
            }
            await _dbcontext.SaveChangesAsync();
        }

        public async Task IdentityDataSeedDataAsync()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new("Admin"));
                    await _roleManager.CreateAsync(new("SuperAdmin"));
                }
                if (!_userManager.Users.Any())
                {
                    ApplicationUser user01 = new()
                    {
                        DisplayName = "Muhammad Ragab",
                        Email = "MuhammadRagab@gmail.com",
                        PhoneNumber = "01101087355",
                        UserName = "MuhammadRagab"
                    };
                    ApplicationUser user02 = new()
                    {
                        DisplayName = "Hadir Muhammad",
                        Email = "HadirMuhammad@gmail.com",
                        PhoneNumber = "01066803012",
                        UserName = "HadirMuhammad"
                    };
                    await _userManager.CreateAsync(user01, "Aaasa1123@!");
                    await _userManager.CreateAsync(user02, "QWWede1@@8");
                    await _userManager.AddToRoleAsync(user01, "Admin");
                    await _userManager.AddToRoleAsync(user02, "SuperAdmin");
                    await _identityStoreDbContext.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {

            }
        }
    }

}
