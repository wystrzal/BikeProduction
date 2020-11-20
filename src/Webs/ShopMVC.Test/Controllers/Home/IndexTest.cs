using Microsoft.AspNetCore.Mvc;
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
using static ShopMVC.Models.Enums.HomeProductEnum;

namespace ShopMVC.Test.Controllers.Home
{
    public class IndexTest
    {
        private readonly Mock<ICatalogService> catalogService;

        private readonly HomeController homeController;
        private readonly List<CatalogProduct> catalogProducts;

        public IndexTest()
        {
            catalogService = new Mock<ICatalogService>();
            homeController = new HomeController(catalogService.Object);
            catalogProducts = new List<CatalogProduct> { new CatalogProduct(), new CatalogProduct() };
        }

        [Fact]
        public async Task Index_Success()
        {
            //Arrange
            catalogService.Setup(x => x.GetHomeProducts(HomeProduct.NewProduct)).Returns(Task.FromResult(catalogProducts));
            catalogService.Setup(x => x.GetHomeProducts(HomeProduct.PopularProduct)).Returns(Task.FromResult(catalogProducts));

            //Act
            var action = await homeController.Index() as ViewResult;
            var model = action.Model as HomePageViewModel;

            //Assert
            Assert.Equal(catalogProducts.Count, model.NewProducts.Count);
            Assert.Equal(catalogProducts.Count, model.PopularProducts.Count);
        }
    }
}
