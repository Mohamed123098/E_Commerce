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
    public class BasketController(IServiceManager _serviceManager):ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<CustomerBasketDTO>> GetBasketAsync(string id)
        {
            var basket = await _serviceManager.BasketService.GetBasketAsync(id);
         //var CustomerBasket= await  GetBasketAsync(id);
            return Ok(basket);
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasketDTO>> CreateOrUpdateAsync(CustomerBasketDTO customerBasketDTO)
        {
            var Basket = await _serviceManager.BasketService.CreateOrUpdateAsync(customerBasketDTO);
            return Ok(Basket);
        }
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasketAsync(string id)
            => Ok(await _serviceManager.BasketService.DeleteBasketAsync(id));
    }
}
