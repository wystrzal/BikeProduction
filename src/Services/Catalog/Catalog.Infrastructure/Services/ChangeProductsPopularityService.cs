using Catalog.Core.Interfaces;
using Catalog.Core.Models.MessagingModels;
using System;
using System.Collections.Generic;
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

        public async Task ChangeProductsPopularity(List<OrderItem> orderItems, bool increasePopularity)
        {
            ValidateOrderItems(orderItems);

            int valueToChangePopularity = SetValueToChangePopularity(increasePopularity);

            await ChangeProductPopularity(orderItems, valueToChangePopularity);
        }

        private void ValidateOrderItems(List<OrderItem> orderItems)
        {
            if (orderItems == null || orderItems.Count <= 0)
            {
                throw new ArgumentNullException();
            }
        }

        private int SetValueToChangePopularity(bool increasePopularity)
        {
            int valueToChangePopularity = 1;

            if (!increasePopularity)
            {
                valueToChangePopularity *= -1;
            }

            return valueToChangePopularity;
        }

        private async Task ChangeProductPopularity(List<OrderItem> orderItems, int valueToChangePopularity)
        {
            foreach (var orderItem in orderItems)
            {
                var product = await productRepository.GetByConditionFirst(x => x.Reference == orderItem.Reference);
                product.Popularity += valueToChangePopularity;
            }

            await productRepository.SaveAllAsync();
        }
    }
}
