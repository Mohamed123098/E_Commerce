using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation
{
    public class ServiceManagerWithFactoryDelegate(
        Func<IProductService> productService,
        Func<IBasketService> basketService,
        Func<IAccountService> accountService,
        Func<IOrderService> orderService)
        : IServiceManager
    {
        public IProductService ProductService => productService.Invoke();

        public IBasketService BasketService => basketService.Invoke();

        public IAccountService AccountService => accountService.Invoke();

        public IOrderService OrderService => orderService.Invoke();
    }
}
