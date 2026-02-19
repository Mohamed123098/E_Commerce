using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductQueryParams
    {
        private  const int DefaultSize = 5;
        private const int MaxSize = 10;
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public ProductSortingOptions sortingOption  { get; set; }
        public string? SearchValue { get; set; }
        public int PageIndex { get; set; }
        private int pageSize;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value>MaxSize?MaxSize: value; }
        }
        //public bool IsPaginated { get; set; }

    }
}
