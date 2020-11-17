using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using ShopMVC.Controllers;
using ShopMVC.Interfaces;
using ShopMVC.Models;
using ShopMVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopMVC.Test.Controllers.Catalog
{
    public class IndexTest
    {
        private readonly Mock<ICatalogService> catalogService;

        private readonly CatalogController catalogController;
        private readonly CatalogProductsViewModel vm;
        private readonly List<CatalogProduct> catalogProducts;

        public IndexTest()
        {
            catalogService = new Mock<ICatalogService>();
            catalogController = new CatalogController(catalogService.Object);
            vm = new CatalogProductsViewModel();
            catalogProducts = new List<CatalogProduct>();
        }

        [Fact]
        public async Task Index_Success()
        {
            //Arrange
            catalogService.Setup(x => x.GetBrandListItem())
                .Returns(Task.FromResult(It.IsAny<IEnumerable<SelectListItem>>()));

            catalogService.Setup(x => x.GetProducts(It.IsAny<CatalogFilteringData>()))
                .Returns(Task.FromResult(catalogProducts));

            //Act
            var action = await catalogController.Index(vm) as ViewResult;
            var model = action.Model as CatalogProductsViewModel;

            //Assert
            Assert.NotNull(model);
        }
    }
}
