using Catalog.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using ShopMVC.Areas.Admin.Controllers;
using ShopMVC.Areas.Admin.Models;
using ShopMVC.Areas.Admin.Models.ViewModels;
using ShopMVC.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopMVC.Test.Controllers.ProductAdmin
{
    public class UpdateProductTest
    {
        private const int productId = 1;

        private readonly Mock<ICatalogService> catalogService;
        private readonly Mock<IWarehouseService> warehouseService;

        private readonly ProductController productController;

        public UpdateProductTest()
        {
            catalogService = new Mock<ICatalogService>();
            warehouseService = new Mock<IWarehouseService>();
            productController = new ProductController(catalogService.Object, warehouseService.Object);
        }

        [Fact]
        public async Task UpdateProduct_Success()
        {
            //Arrange
            IEnumerable<SelectListItem> brandListItem = new List<SelectListItem> { new SelectListItem(), new SelectListItem() };
            var parts = new List<Part> { new Part(), new Part() };

            catalogService.Setup(x => x.GetProduct(productId)).Returns(Task.FromResult(new Models.CatalogProduct { Id = productId, Reference = "" }));
            catalogService.Setup(x => x.GetBrandListItem()).Returns(Task.FromResult(brandListItem));
            warehouseService.Setup(x => x.GetProductParts("")).Returns(Task.FromResult(parts));

            //Act
            var action = await productController.UpdateProduct(productId) as ViewResult;
            var model = action.Model as PostPutProductViewModel;

            //Assert
            Assert.Equal(brandListItem, model.Brand);
            Assert.Equal(productId, model.Product.Id);
            Assert.Equal(parts.Count, model.Parts.Count);
        }

        [Fact]
        public async Task UpdateProduct_PostAction_NotValidModel_Success()
        {
            //Arrange
            productController.ModelState.AddModelError("", "");
            IEnumerable<SelectListItem> brandListItem = new List<SelectListItem> { new SelectListItem(), new SelectListItem() };
            var parts = new List<Part> { new Part(), new Part() };
            var product = new Models.CatalogProduct { Id = productId, Reference = "" };

            catalogService.Setup(x => x.GetProduct(productId)).Returns(Task.FromResult(product));
            catalogService.Setup(x => x.GetBrandListItem()).Returns(Task.FromResult(brandListItem));
            warehouseService.Setup(x => x.GetProductParts("")).Returns(Task.FromResult(parts));

            //Act
            var action = await productController.UpdateProduct(product) as ViewResult;
            var model = action.Model as PostPutProductViewModel;

            //Assert
            Assert.Equal(brandListItem, model.Brand);
            Assert.Equal(productId, model.Product.Id);
            Assert.Equal(parts.Count, model.Parts.Count);
        }

        [Fact]
        public async Task UpdateProduct_PostAction_ValidModel_Success()
        {
            //Arrange
            var product = new Models.CatalogProduct { Id = productId, Reference = "" };

            //Act
            var action = await productController.UpdateProduct(product) as RedirectToActionResult;

            //Assert
            Assert.Equal("Index", action.ActionName);
            catalogService.Verify(x => x.UpdateProduct(product), Times.Once);
        }
    }
}