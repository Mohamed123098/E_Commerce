using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using ServiceAbstraction;
using Shared;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation
{
    public class ProductService(IUnitOfWork _unitOfWork,IMapper _mapper) : IProductService
    {
        public async Task<PaginationResult<ProductDTO>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
           var ProductRepo= _unitOfWork.GetRepository<Product, int>();
            var products = await ProductRepo.GetAllAsync(new ProductTypeAndBrandSpecifications(queryParams));
             var Data = _mapper.Map<IEnumerable<Product>,IEnumerable<ProductDTO>>(products);
            var TotalCount =await ProductRepo.GetAllAsync(new TotalProdunctSpecifications(queryParams));
            return new PaginationResult<ProductDTO>() { Data = Data, PageSize = Data.Count(), PageIndex = queryParams.PageIndex, TotalCount =  TotalCount.Count()};
        }

        public async Task<IEnumerable<ProductBrandDTO>> GetAllBrandsAsync()
        {
           var Brands= await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<ProductBrandDTO>>(Brands);
        }

        public async Task<IEnumerable<ProductTypeDTO>> GetAllTypesAsync()
        {
          var Types=await  _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductType>, IEnumerable<ProductTypeDTO>>(Types);
        }

        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
           var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(new ProductTypeAndBrandSpecifications(id));
            return _mapper.Map<Product, ProductDTO>(product);
        }

      
    }
}
