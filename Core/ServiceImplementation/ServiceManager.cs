using AutoMapper;
using Domain.Contracts;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation
{
    public class ServiceManager(IUnitOfWork unitOfWork,IMapper mapper) : IServiceManager
    {
        private readonly Lazy<IProductService> productService =new Lazy<IProductService>(()=>new ProductService(unitOfWork,mapper));
        public IProductService ProductService => productService.Value;
    }
}
