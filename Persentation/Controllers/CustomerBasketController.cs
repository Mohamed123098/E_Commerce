using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DTOs.BasketDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CustomerBasketController(IServiceManager _serviceManager):ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<CustomerBasketDTO>> GetBasketAsync(string id)
        {
         var CustomerBasket= await  basketCustomerService.GetBasketAsync(id);
            return Ok(CustomerBasket);
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasketDTO>> CreateOrUpdateAsync(CustomerBasketDTO customerBasketDTO)
        {
            var customerBasket = await basketCustomerService.CreateOrUpdateAsync(customerBasketDTO);
            return Ok(customerBasket);
        }
    }
}
