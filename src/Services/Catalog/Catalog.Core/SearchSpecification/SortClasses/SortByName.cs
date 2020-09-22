using BikeSortFilter;
using Catalog.Core.Models;
using System;

namespace Catalog.Core.SearchSpecification.SortClasses
{
    public class SortByName : IConcreteSort<Product, string>
    {
        public Func<Product, string> GetSortCondition()
        {
            return x => x.ProductName;
        }
    }
}

