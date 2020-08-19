using BikeSortFilter;
using Catalog.Core.Models;
using System;

namespace Catalog.Core.SearchSpecification.SortClasses
{
    public class SortByDate : IConcreteSort<Product, DateTime>
    {
        public Func<Product, DateTime> GetConcreteSort()
        {
            return x => x.DateAdded;
        }
    }
}
