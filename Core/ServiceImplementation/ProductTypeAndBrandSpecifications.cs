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
    class ProductTypeAndBrandSpecifications : BaseSpecification<Product, int>
    {
        public ProductTypeAndBrandSpecifications(ProductQueryParams queryParams) :
            base(p => (!queryParams.BrandId.HasValue || p.BrandId == queryParams.BrandId) && (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId)
            && (string.IsNullOrWhiteSpace(queryParams.SearchValue)|| p.Name.ToLower().Contains(queryParams.SearchValue.ToLower())))
        {
            
            AddInclude(p => p.Brand);
            AddInclude(p => p.Type);
            switch (queryParams.sortingOption)
            {
                case ProductSortingOptions.NameASC:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDEC:
                    AddOrderByDescending(p => p.Name);
                    break;
                case ProductSortingOptions.PriceASC:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDEC:
                    AddOrderByDescending(p => p.Price);
                    break;

            }
                ApplyPagination(queryParams.PageIndex,queryParams.PageSize);

        }
        public ProductTypeAndBrandSpecifications(int id) :
            base(p => p.Id ==id)
        {
            AddInclude(p => p.Brand);
            AddInclude(p => p.Type);

        }
    }
}
