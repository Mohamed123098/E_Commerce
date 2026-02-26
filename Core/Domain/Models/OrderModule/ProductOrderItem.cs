using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.OrderModule
{
    public class ProductOrderItem
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string PictureURL { get; set; }
    }
}
