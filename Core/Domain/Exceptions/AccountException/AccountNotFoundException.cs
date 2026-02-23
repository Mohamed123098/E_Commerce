using Domain.Exceptions.ProductExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.AccountException
{
    public sealed class AccountNotFoundException(string email):NotFoundException($"Account with email {email} not found")
    {
    }
}
