using BikeSortFilter;
using Delivery.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delivery.Core.SearchSpecification.FilterClasses
{
    public class FilterByLoadingPlaceStatus : ConcreteFilter<LoadingPlace, LoadingPlaceFilteringData>
    {
        public FilterByLoadingPlaceStatus(LoadingPlaceFilteringData filteringData) : base(filteringData)
        {

        }

        public override Predicate<LoadingPlace> GetFilteringCondition()
        {
            return x => x.LoadingPlaceStatus == filteringData.LoadingPlaceStatus;
        }
    }
}
