using Shared.DTOs.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.OrderDTO
{
    public class OrderToReturnDTO
    {
        public string BasketId { get; set; }
        public AddressDTO ShippingAddress { get; set; }
        public int DeliveryMethodId { get; set; }
        public string DeliveryMethod { get; set; } = default!;
        public string OrderStatus { get; set; } = default!;
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
    }
}
