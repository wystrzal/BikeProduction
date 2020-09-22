using BikeSortFilter;
using Catalog.Core.Models;
using System;

namespace Catalog.Core.SearchSpecification.SortClasses
{
    public class SortByPopularity : IConcreteSort<Product, int>
    {
        public Func<Product, int> GetSortCondition()
        {
            return x => x.Popularity;
        }
    }
}

