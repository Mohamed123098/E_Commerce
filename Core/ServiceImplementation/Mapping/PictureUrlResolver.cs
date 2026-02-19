using AutoMapper;
using AutoMapper.Execution;
using AutoMapper.Internal;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation.Mapping
{
    public class PictureUrlResolver(IConfiguration _configuration) : IValueResolver<Product, ProductDTO, string>
    {
        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureURL))
                return "";
            else
                return $"{_configuration["Urls:BaseUrl"]}{source.PictureURL}";
        }
    }
}
