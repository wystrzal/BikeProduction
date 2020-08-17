using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application.Mapping
{
    public class GetHomeProductsDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string PhotoUrl { get; set; }
    }
}
