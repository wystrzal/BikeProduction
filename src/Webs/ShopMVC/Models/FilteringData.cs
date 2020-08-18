using System;
using System.Collections.Generic;
using System.Text;
using static ShopMVC.Models.Enums.BikeTypeEnum;
using static ShopMVC.Models.Enums.ColorsEnum;
using static ShopMVC.Models.Enums.SortEnum;

namespace ShopMVC.Models
{
    public class FilteringData
    {
        public Sort Sort { get; set; }
        public Colors Colors { get; set; }
        public BikeType BikeType { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public int BrandId { get; set; }
    }
}

