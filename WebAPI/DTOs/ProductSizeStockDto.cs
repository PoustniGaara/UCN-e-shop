using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.DTOs
{
    public class ProductSizeStockDto
    {
        public int Id { get; set; }
        public string Size { get; set; }
        public int Stock { get; set; }
    }
}
