using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class CreateProductTest
    {
        private readonly Mock<ICatalogService> catalogService;
        private readonly Mock<IWarehouseService> warehouseService;

        private readonly ProductController productController;

        public CreateProductTest()
        {
            catalogService = new Mock<ICatalogService>();
            warehouseService = new Mock<IWarehouseService>();
            productController = new ProductController(catalogService.Object, warehouseService.Object);
        }

        [Fact]
        public async Task CreateProduct_Success()
        {
            //Arrange
            IEnumerable<SelectListItem> brandListItem = new List<SelectListItem> { new SelectListItem(), new SelectListItem() };

            catalogService.Setup(x => x.GetBrandListItem()).Returns(Task.FromResult(brandListItem));

            //Act
            var action = await productController.CreateProduct() as ViewResult;
            var model = action.Model as PostPutProductViewModel;

            //Assert
            Assert.Equal(brandListItem, model.Brand);
            Assert.NotNull(model.Product);
            Assert.Null(model.Parts);
        }

        [Fact]
        public async Task CreateProduct_PostAction_NotValidModel_Success()
        {
            //Arrange
            productController.ModelState.AddModelError("", "");
            IEnumerable<SelectListItem> brandListItem = new List<SelectListItem> { new SelectListItem(), new SelectListItem() };

            catalogService.Setup(x => x.GetBrandListItem()).Returns(Task.FromResult(brandListItem));

            //Act
            var action = await productController.CreateProduct(It.IsAny<CatalogProduct>()) as ViewResult;
            var model = action.Model as PostPutProductViewModel;

            //Assert
            Assert.Equal(brandListItem, model.Brand);
            Assert.NotNull(model.Product);
            Assert.Null(model.Parts);
        }

        [Fact]
        public async Task CreateProduct_PostAction_ValidModel_Success()
        {
            //Arrange
            var catalogProduct = new CatalogProduct();

            //Act
            var action = await productController.CreateProduct(catalogProduct) as RedirectToActionResult;

            //Assert
            catalogService.Verify(x => x.AddProduct(catalogProduct), Times.Once);
            Assert.Equal("Index", action.ActionName);
        }
    }
}