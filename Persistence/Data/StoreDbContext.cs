using Domain.Models.ProductModule;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Persentation.Data
{
    public class StoreDbContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands  { get; set; }
        public DbSet<Product> ProductTypes { get; set; }
        public StoreDbContext(DbContextOptions<StoreDbContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(assembly: typeof(AssemblyReference).Assembly);
        }
    }
}
