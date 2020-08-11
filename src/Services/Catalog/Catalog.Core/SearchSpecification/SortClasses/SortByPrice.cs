using BikeSortFilter;
using Catalog.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
