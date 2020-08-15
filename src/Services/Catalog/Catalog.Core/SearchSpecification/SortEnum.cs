using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Core.SearchSpecification
{
    public class SortEnum
    {
        public enum Sort
        {
            SortByPriceAsc = 1,
            SortByPriceDesc = 2,
            SortByDateAsc = 3,
            SortByDateDesc = 4,
        }
    }
}
