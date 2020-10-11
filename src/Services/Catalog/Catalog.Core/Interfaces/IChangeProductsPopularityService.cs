using Catalog.Core.Models.MessagingModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Core.Interfaces
{
    public interface IChangeProductsPopularityService
    {
        Task ChangeProductsPopularity(List<OrderItem> orderItems, bool increasePopularity);
    }
}
