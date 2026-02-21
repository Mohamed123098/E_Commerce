using Domain.Models.ProductModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persentation.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.Brand).WithMany(brand => brand.Products).HasForeignKey(p => p.BrandId);
            builder.HasOne(p => p.Type).WithMany(type => type.Products).HasForeignKey(p => p.TypeId);
        
        
        }
    }
}
