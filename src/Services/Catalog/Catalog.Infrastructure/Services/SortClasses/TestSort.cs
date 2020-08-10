using BikeSortFilter;
using Catalog.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Infrastructure.Services.SortClasses
{
    public class TestSort : IConcreteSort<Product, int>
    {
        public Func<Product, int> GetConcreteSort()
        {
            return x => x.Id;
        }
    }
}
