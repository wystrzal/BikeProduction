using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMVC.Areas.Admin.Models.Dto
{
    public class AddProductPartDto
    {
        public string Reference { get; set; }
        public int PartId { get; set; }
        public int ProductId { get; set; }
    }
}
