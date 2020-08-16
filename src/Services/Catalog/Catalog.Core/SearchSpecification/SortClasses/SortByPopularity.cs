using BikeSortFilter;
using Catalog.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Core.SearchSpecification.SortClasses
{
    public class SortByPopularity : IConcreteSort<Product, int>
    {
        public Func<Product, int> GetConcreteSort()
        {
            return x => x.Popularity;
        }
    }
}
