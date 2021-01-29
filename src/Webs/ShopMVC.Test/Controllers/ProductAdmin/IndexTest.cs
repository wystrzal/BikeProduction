using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopMVC.Areas.Admin.Controllers;
using ShopMVC.Areas.Admin.Models.ViewModels;
using ShopMVC.Interfaces;
using ShopMVC.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopMVC.Test.Controllers.ProductAdmin
{
    public class IndexTest
    {
        private readonly Mock<ICatalogService> catalogService;
        private readonly Mock<IWarehouseService> warehouseService;

        private readonly ProductController productController;

        public IndexTest()
        {
            catalogService = new Mock<ICatalogService>();
            warehouseService = new Mock<IWarehouseService>();
            productController = new ProductController(catalogService.Object, warehouseService.Object);
        }

        [Fact]
        public async Task Index_Success()
        {
            //Arrange
            var products = new List<CatalogProduct> { new CatalogProduct(), new CatalogProduct() };
            catalogService.Setup(x => x.GetProducts(It.IsAny<CatalogFilteringData>())).Returns(Task.FromResult(products));

            //Act
            var action = await productController.Index() as ViewResult;
            var model = action.Model as ProductsViewModel;

            //Assert
            Assert.Equal(products.Count, model.Products.Count);
        }
    }
}