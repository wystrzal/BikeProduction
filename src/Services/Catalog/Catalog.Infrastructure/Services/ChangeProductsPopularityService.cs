using Catalog.Core.Interfaces;
using Catalog.Core.Models.MessagingModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Services
{
    public class ChangeProductsPopularityService : IChangeProductsPopularityService
    {
        private readonly IProductRepository productRepository;

        public ChangeProductsPopularityService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<bool> ChangeProductsPopularity(List<OrderItem> orderItems, bool increasePopularity)
        {
            if (orderItems == null || orderItems.Count <= 0)
                return false;

            int valueToChangePopularity = 1;

            if (!increasePopularity)
                valueToChangePopularity *= -1;

            foreach (var orderItem in orderItems)
            {
                var product = await productRepository.GetByConditionFirst(x => x.Reference == orderItem.Reference);
                product.Popularity += valueToChangePopularity;
            }

            return await productRepository.SaveAllAsync();
        }
    }
}
