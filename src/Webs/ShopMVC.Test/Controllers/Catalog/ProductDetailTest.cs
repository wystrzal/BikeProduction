using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopMVC.Controllers;
using ShopMVC.Interfaces;
using ShopMVC.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopMVC.Test.Controllers.Catalog
{
    public class ProductDetailTest
    {
        private const int id = 1;

        private readonly Mock<ICatalogService> catalogService;

        private readonly CatalogController catalogController;
        private readonly CatalogProduct catalogProduct;

        public ProductDetailTest()
        {
            catalogService = new Mock<ICatalogService>();
            catalogController = new CatalogController(catalogService.Object);
            catalogProduct = new CatalogProduct { Id = id };
        }

        [Fact]
        public async Task ProductDetail_Success()
        {
            //Arrange
            catalogService.Setup(x => x.GetProduct(id)).Returns(Task.FromResult(catalogProduct));

            //Act
            var action = await catalogController.ProductDetail(id) as ViewResult;
            var model = action.Model as CatalogProduct;

            //Assert
            Assert.Equal(id, model.Id);
        }
    }
}
