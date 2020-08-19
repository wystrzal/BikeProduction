using BikeSortFilter;
using Catalog.Core.Models;
using System;

namespace Catalog.Core.SearchSpecification.SortClasses
{
    public class SortByPrice : IConcreteSort<Product, decimal>
    {
        public Func<Product, decimal> GetConcreteSort()
        {
            return x => x.Price;
        }
    }
}
