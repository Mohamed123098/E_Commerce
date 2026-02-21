using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.BasketDTO
{
    public class CustomerBasketDTO
    {
        public string Id { get; set; }
        public ICollection<BasketItemsDTO> BasketItemsDTO { get; set; }
    }
}
