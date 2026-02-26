using Shared.DTOs.OrderDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IOrderService
    {
        Task<OrderToReturnDTO> CreateOrderAsync(OrderDTO orderDTO,string email);

        Task<IEnumerable<DeliveryMethodsDTO>> GetAllDeliveryMethods();

        Task<IEnumerable<OrderToReturnDTO>> GetAllOrders(string email);

        Task<OrderToReturnDTO> GetOrderById(Guid id);
    }
}
