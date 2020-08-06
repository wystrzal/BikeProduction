using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMVC.Models.Dtos
{
    public class GetProductsDto
    {
        public int Take { get; set; }
        public int Skip { get; set; }
    }
}
