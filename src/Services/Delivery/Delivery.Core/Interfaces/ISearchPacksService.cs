using Delivery.Core.Models;
using Delivery.Core.SearchSpecification;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Core.Interfaces
{
    public interface ISearchPacksService
    {
        Task<List<PackToDelivery>> SearchPacks(FilteringData filteringData);
    }
}
