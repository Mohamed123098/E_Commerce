using AutoMapper;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation.Mapping
{
    public class ProductProfile:Profile
    {
        public ProductProfile(IConfiguration configuration)
        {
            CreateMap<Product, ProductDTO>().ForMember(p => p.BrandName, options => options.MapFrom(p => p.Brand.Name))
                .ForMember(p => p.TypeName, options => options.MapFrom(p => p.Type.Name)).
                ForMember(p=>p.PictureURL,options=>options.MapFrom(new PictureUrlResolver(configuration)));
            CreateMap<ProductBrand, ProductBrandDTO>();
            CreateMap<ProductType, ProductTypeDTO>();
         
        }
    }
}
