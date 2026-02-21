using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.ProductExceptions
{
    public abstract class NotFoundException(string Message):Exception(Message)
    {
    }
}
