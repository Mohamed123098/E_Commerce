using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions.BasketExcep;
using Domain.Models.OrderModule;
using Domain.Models.ProductModule;
using ServiceAbstraction;
using ServiceImplementation.Specifications.OrderSpecs;
using Shared.DTOs.Account;
using Shared.DTOs.OrderDTO;

namespace ServiceImplementation
{
    public class OrderService(IMapper _mapper,IBasketRepository _repository,IUnitOfWork _unitOfWork) : IOrderService
    {
        public async Task<OrderToReturnDTO> CreateOrderAsync(OrderDTO orderDTO,string email)
        {
            var address = _mapper.Map<AddressDTO, OrderAddress>(orderDTO.Address);
            var basket =await _repository.GetBasketAsync(orderDTO.BasketId)??throw new CustomerBasketNotFound(orderDTO.BasketId);
            List<OrderItems> orderItems = [];
            var ProductRepo = _unitOfWork.GetRepository<Product, int>();
            foreach (var item in basket.BasketItems)
            {
                var product = await ProductRepo.GetByIdAsync(item.Id)??throw new Exception();
                var Items = new OrderItems()
                {
                    OrderedItem = new() { Name = product?.Name, PictureURL = product?.PictureURL, ProductId = product.Id },
                    Price =product.Price,
                    Quantity = item.Quantity
                };
                orderItems.Add(Items);
            }
            var DeliveryMethod =await _unitOfWork.GetRepository<OrderDeliveryMethod, int>().GetByIdAsync(orderDTO.DeliveryMethodId);
            var order = new Order()
            {
                Items=orderItems,
                UserEmail=email,
                ShippingAddress =address,
                DeliveryMethod =DeliveryMethod,
                SubTotal = orderItems.Sum(O=>O.Price*O.Quantity)
            };
            await _unitOfWork.GetRepository<Order, Guid>().AddAsync(order);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<Order, OrderToReturnDTO>(order);
        }

        public async Task<IEnumerable<DeliveryMethodsDTO>> GetAllDeliveryMethods()
        {
            var DeliveryMethods =await _unitOfWork.GetRepository<OrderDeliveryMethod, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<OrderDeliveryMethod>, IEnumerable<DeliveryMethodsDTO>>(DeliveryMethods);
        }

        public async Task<IEnumerable<OrderToReturnDTO>> GetAllOrders(string email)
        {
            var specs = new OrderSpecifications(email);
            var Orders = await _unitOfWork.GetRepository<Order, Guid>().GetAllAsync(specs);
            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderToReturnDTO>>(Orders);
        }

        public async Task<OrderToReturnDTO> GetOrderById(Guid id)
        {
            var specs = new OrderSpecifications(id);
            var Order = await _unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(specs);
            return _mapper.Map<Order, OrderToReturnDTO>(Order);
            
        }
    }
}
