using BikeSortFilter;
using Catalog.Core.Models;
using Catalog.Core.SearchSpecification;
using Catalog.Infrastructure.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using static Catalog.Core.Models.Enums.ColorsEnum;

namespace Catalog.Test.Services
{
    public class SearchProductServiceTest
    {
        private const Colors color = Colors.Black;

        private readonly Mock<ISearchSortFilterService<Product, FilteringData>> sortFilterService;

        private readonly SearchProductsService service;
        private readonly List<Product> products;
        private readonly FilteringData filteringData;

        public SearchProductServiceTest()
        {
            sortFilterService = new Mock<ISearchSortFilterService<Product, FilteringData>>();
            service = new SearchProductsService(sortFilterService.Object);
            products = new List<Product> { new Product(), new Product() };
            filteringData = new FilteringData { Colors = color };
        }

        [Fact]
        public async Task GetProducts_Success()
        {
            //Arrange
            sortFilterService.Setup(x => x.Search(It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(products));

            //Act
            var action = await service.GetProducts(filteringData);

            //Assert
            sortFilterService.Verify(x => x.SetConcreteSort(It.IsAny<Func<Product, string>>()), Times.Once);
            sortFilterService.Verify(x => x.SetConcreteFilter(It.IsAny<Predicate<Product>>()), Times.Once);

            Assert.Equal(products.Count, action.Count);
        }
    }
}
