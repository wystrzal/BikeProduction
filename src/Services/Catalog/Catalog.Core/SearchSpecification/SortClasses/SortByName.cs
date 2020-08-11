using BikeSortFilter;
using Catalog.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Core.SearchSpecification.SortClasses
{
    public class SortByName : IConcreteSort<Product, string>
    {
        public Func<Product, string> GetConcreteSort()
        {
            return x => x.ProductName;
        }
    }
}
