using Basket.Core.Interfaces;
using Basket.Infrastructure.Services;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Basket.Test
{
    public class ServicesTest
    {
        private readonly Mock<IDistributedCache> distributedCache;
        public ServicesTest()
        {
            distributedCache = new Mock<IDistributedCache>();
        }

        [Fact]
        public async Task CheckIfUserIdIsNull_ThrowsArgumentNullException()
        {
            //Arrange
            var service = new BasketRedisService(distributedCache.Object);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => service.GetBasket(null));
        }
    }
}
