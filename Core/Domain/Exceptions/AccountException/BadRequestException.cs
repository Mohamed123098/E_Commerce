using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.AccountException
{
    public sealed class BadRequestException(string email):Exception($"can't create account with this email {email}")
    {
    }
}
