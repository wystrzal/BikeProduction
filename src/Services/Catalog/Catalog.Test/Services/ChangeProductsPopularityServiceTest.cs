using Catalog.Core.Interfaces;
using Catalog.Core.Models;
using Catalog.Core.Models.MessagingModels;
using Catalog.Infrastructure.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Catalog.Test.Services
{
    public class ChangeProductsPopularityServiceTest
    {
        private readonly Mock<IProductRepository> productRepository;

        private readonly ChangeProductsPopularityService service;
        private readonly List<OrderItem> orderItems;

        public ChangeProductsPopularityServiceTest()
        {
            productRepository = new Mock<IProductRepository>();
            service = new ChangeProductsPopularityService(productRepository.Object);
            orderItems = new List<OrderItem> { new OrderItem() };
        }

        [Fact]
        public async Task ChangeProductsPopularity_Success()
        {
            //Arrange
            productRepository.Setup(x => x.GetByConditionFirst(It.IsAny<Func<Product, bool>>()))
                .Returns(Task.FromResult(new Product()));

            //Act
            await service.ChangeProductsPopularity(orderItems, It.IsAny<bool>());

            //Assert
            productRepository.Verify(x => x.SaveAllAsync(), Times.Once);
        }

        [Fact]
        public async Task ChangeProductsPopularity_ThrowsArgumentNullException()
        {
            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                service.ChangeProductsPopularity(It.IsAny<List<OrderItem>>(), It.IsAny<bool>()));   
        }
    }
}
