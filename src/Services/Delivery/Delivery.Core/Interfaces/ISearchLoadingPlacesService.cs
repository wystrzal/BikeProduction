using Delivery.Core.Models;
using Delivery.Core.SearchSpecification;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Core.Interfaces
{
    public interface ISearchLoadingPlacesService
    {
        Task<List<LoadingPlace>> SearchLoadingPlaces(LoadingPlaceFilteringData filteringData);
    }
}
