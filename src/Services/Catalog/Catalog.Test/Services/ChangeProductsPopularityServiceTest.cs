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

        public ChangeProductsPopularityServiceTest()
        {
            productRepository = new Mock<IProductRepository>();
        }
        [Fact]
        public async Task ChangeProductsPopularity_Success()
        {
            //Arrange
            var orderItems = new List<OrderItem> { new OrderItem() };

            productRepository.Setup(x => x.GetProductByReference(It.IsAny<string>())).Returns(Task.FromResult(new Product()));
            productRepository.Setup(x => x.SaveAllAsync()).Returns(Task.FromResult(true)).Verifiable();

            var service = new ChangeProductsPopularityService(productRepository.Object);

            //Act
            var action = await service.ChangeProductsPopularity(orderItems, It.IsAny<bool>());

            //Assert
            Assert.True(action);
            productRepository.Verify(x => x.SaveAllAsync(), Times.Once);
        }

        [Fact]
        public async Task ChangeProductsPopularity_Failed()
        {
            //Arrange
            var orderItems = new List<OrderItem>();

            var service = new ChangeProductsPopularityService(productRepository.Object);

            //Act
            var action = await service.ChangeProductsPopularity(orderItems, It.IsAny<bool>());

            //Assert
            Assert.False(action);
        }
    }
}
