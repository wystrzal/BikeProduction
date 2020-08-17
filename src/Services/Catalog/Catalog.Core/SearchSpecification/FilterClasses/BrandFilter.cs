﻿using BikeSortFilter;
using Catalog.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Core.SearchSpecification.FilterClasses
{
    public class BrandFilter : IConcreteFilter<Product>
    {
        private readonly FilteringData filteringData;

        public BrandFilter(FilteringData filteringData)
        {
            this.filteringData = filteringData;
        }

        public Predicate<Product> GetConcreteFilter()
        {
            return x => x.BrandId == filteringData.BrandId;
        }
    }
}