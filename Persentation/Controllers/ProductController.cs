using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IServiceManager _serviceManager):ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginationResult<ProductDTO>>> GetAllProducts([FromQuery]ProductQueryParams queryParams)   
            => Ok(await _serviceManager.ProductService.GetAllProductsAsync(queryParams));
        
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
            => Ok(await _serviceManager.ProductService.GetProductByIdAsync(id));
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<ProductBrandDTO>>> GetAllBrands()
            => Ok(await _serviceManager.ProductService.GetAllBrandsAsync());
        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<ProductTypeDTO>>> GetAllTypes()
            => Ok(await _serviceManager.ProductService.GetAllTypesAsync());

   

    }
}
