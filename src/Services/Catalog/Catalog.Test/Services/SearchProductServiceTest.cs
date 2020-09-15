using BikeSortFilter;
using Catalog.Core.Models;
using Catalog.Core.SearchSpecification;
using Catalog.Core.SearchSpecification.FilterClasses;
using Catalog.Core.SearchSpecification.SortClasses;
using Catalog.Infrastructure.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Catalog.Test.Services
{
    public class SearchProductServiceTest
    {
        private readonly Mock<ISortFilterService<Product, FilteringData>> sortFilterService;

        public SearchProductServiceTest()
        {
            sortFilterService = new Mock<ISortFilterService<Product, FilteringData>>();
        }

        [Fact]
        public async Task GetProducts_Success()
        {
            //Arrange
            var products = new List<Product>
            {
                new Product { Id = 1 },
                new Product { Id = 2 }
            };

            var filteringData = new FilteringData { Colors = Core.Models.Enums.ColorsEnum.Colors.Black };

            sortFilterService.Setup(x => x.SetConcreteSort<SortByName, string>()).Verifiable();
            sortFilterService.Setup(x => x.SetConcreteFilter<ColorFilter>(filteringData)).Verifiable();

            sortFilterService.Setup(x => x.Search(It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult(products));

            var service = new SearchProductService(sortFilterService.Object);

            //Act
            var action = await service.GetProducts(filteringData);

            //Assert
            sortFilterService.Verify(x => x.SetConcreteSort<SortByName, string>(), Times.Once);
            sortFilterService.Verify(x => x.SetConcreteFilter<ColorFilter>(filteringData), Times.Once);

            Assert.Equal(2, action.Count);
            Assert.Equal(1, action.Select(x => x.Id).First());
        }
    }
}
