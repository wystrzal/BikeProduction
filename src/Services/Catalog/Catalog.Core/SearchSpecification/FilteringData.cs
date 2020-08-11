using System;
using System.Collections.Generic;
using System.Text;
using static Catalog.Core.Models.ColorsEnum;
using static Catalog.Core.SearchSpecification.SortEnum;

namespace Catalog.Core.SearchSpecification
{
    public class FilteringData
    {
        public int Id { get; set; }
        public Sort Sort { get; set; }
        public Colors Colors { get; set; }
    }
}
