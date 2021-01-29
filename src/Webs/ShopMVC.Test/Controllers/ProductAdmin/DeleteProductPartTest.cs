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
    public class DeleteProductPartTest
    {
        private readonly Mock<ICatalogService> catalogService;
        private readonly Mock<IWarehouseService> warehouseService;

        private readonly ProductController productController;

        public DeleteProductPartTest()
        {
            catalogService = new Mock<ICatalogService>();
            warehouseService = new Mock<IWarehouseService>();
            productController = new ProductController(catalogService.Object, warehouseService.Object);
        }

        [Fact]
        public async Task DeleteProductPart_Success()
        {
            //Act
            var action = await productController.DeleteProductPart(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()) as RedirectToActionResult;

            //Assert
            warehouseService.Verify(x => x.DeleteProductPart(It.IsAny<string>(), It.IsAny<int>()), Times.Once);
            Assert.Equal("UpdateProduct", action.ActionName);
        }
    }
}