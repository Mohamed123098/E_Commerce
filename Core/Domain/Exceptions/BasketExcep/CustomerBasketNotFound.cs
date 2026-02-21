using Domain.Exceptions.ProductExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.BasketExcep
{
    public sealed class CustomerBasketNotFound(string id):NotFoundException($"Customer basket with id {id} is not found")
    {
    }
}
