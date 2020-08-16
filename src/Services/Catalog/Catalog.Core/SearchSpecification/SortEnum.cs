﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Core.SearchSpecification
{
    public class SortEnum
    {
        public enum Sort
        {
            Price_Ascending = 1,
            Price_Descending = 2,
            Oldest_Added = 3,
            Latest_Added = 4,
            The_Most_Popular = 5,
            The_Least_Popular = 6
        }
    }
}
