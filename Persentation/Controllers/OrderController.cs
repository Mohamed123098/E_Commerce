using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DTOs.OrderDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persentation.Controllers
{
    [Authorize]
    public class OrderController(IServiceManager _serviceManager):BaseApiController
    {
        
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDTO>> CreateOrderAsync(OrderDTO orderDTO)
        {
         var user =  await _serviceManager.OrderService.CreateOrderAsync(orderDTO,GetUserEmail());
            return Ok(user);
        }
        [AllowAnonymous]
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodsDTO>>> GetAllDeliveryMethodsAsync()
            => Ok(await _serviceManager.OrderService.GetAllDeliveryMethods());
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeliveryMethodsDTO>>> GetAllOrdersAsync()
            => Ok(await _serviceManager.OrderService.GetAllOrders(GetUserEmail()));
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodsDTO>>> GetOrderByIdAsync(Guid id)
            => Ok(await _serviceManager.OrderService.GetOrderById(id));

    }
}
