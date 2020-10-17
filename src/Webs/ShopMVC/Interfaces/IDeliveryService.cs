using ShopMVC.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMVC.Interfaces
{
    public interface IDeliveryService
    {
        Task<List<PackToDelivery>> GetPacks(PackFilteringData filteringData);
    }
}
