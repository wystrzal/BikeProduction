using System;
using System.Collections.Generic;
using System.Text;
using static Catalog.Core.Models.ColorsEnum;
using static Catalog.Core.Models.Enums.BikeTypeEnum;
using static Catalog.Core.SearchSpecification.SortEnum;

namespace Catalog.Core.SearchSpecification
{
    public class FilteringData
    {
        public Sort Sort { get; set; }
        public Colors Colors { get; set; }
        public BikeType BikeType { get; set; }
        public int BrandId { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
    }
}
