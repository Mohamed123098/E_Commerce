using Domain.Models;
using ServiceImplementation.Specifications;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation
{
     class TotalProdunctSpecifications:BaseSpecification<Product,int>
    {
        public TotalProdunctSpecifications(ProductQueryParams queryParams) : base(
            (p => (!queryParams.BrandId.HasValue || p.BrandId == queryParams.BrandId) && (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId)
            && (string.IsNullOrWhiteSpace(queryParams.SearchValue) || p.Name.ToLower().Contains(queryParams.SearchValue.ToLower()))))
        {

        }
    }
}
