using Domain.Exceptions.ProductExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.AccountException
{
    public sealed class AddressNotFoundException(string email):NotFoundException($"this account with email [{email}] has no value in address")
    {
    }
}
