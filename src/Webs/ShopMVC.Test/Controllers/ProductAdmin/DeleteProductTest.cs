using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopMVC.Areas.Admin.Controllers;
using ShopMVC.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopMVC.Test.Controllers.ProductAdmin
{
    public class DeleteProductTest
    {
        private readonly Mock<ICatalogService> catalogService;
        private readonly Mock<IWarehouseService> warehouseService;

        private readonly ProductController productController;

        public DeleteProductTest()
        {
            catalogService = new Mock<ICatalogService>();
            warehouseService = new Mock<IWarehouseService>();
            productController = new ProductController(catalogService.Object, warehouseService.Object);
        }

        [Fact]
        public async Task DeleteProduct_Success()
        {
            //Act
            var action = await productController.DeleteProduct(It.IsAny<int>()) as RedirectToActionResult;

            //Assert
            catalogService.Verify(x => x.DeleteProduct(It.IsAny<int>()), Times.Once);
            Assert.Equal("Index", action.ActionName);
        }
    }
}