using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.OrderModule
{
    public class Order:BaseEntity<Guid>
    {
        
        public string UserEmail { get; set; }
        public OrderAddress ShippingAddress { get; set; }
        public decimal SubTotal { get; set; }
        public DateTimeOffset OrderData { get; set; } = DateTimeOffset.UtcNow;
        public ICollection<OrderItems> Items { get; set; } = [];
        public decimal GetTotal() => SubTotal + DeliveryMethod.Cost;
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public OrderDeliveryMethod DeliveryMethod { get; set; }
        public int DeliveryMethodId { get; set; }//fk

    }
}
