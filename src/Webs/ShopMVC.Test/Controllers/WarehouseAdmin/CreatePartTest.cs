using Microsoft.AspNetCore.Mvc;
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

namespace ShopMVC.Test.Controllers.WarehouseAdmin
{
    public class CreatePartTest
    {
        private const int partId = 1;

        private readonly Mock<IWarehouseService> warehouseService;

        private readonly WarehouseController warehouseController;
        private readonly Part part;

        public CreatePartTest()
        {
            warehouseService = new Mock<IWarehouseService>();
            warehouseController = new WarehouseController(warehouseService.Object);
            part = new Part { Id = partId };
        }

        [Fact]
        public async Task CreatePart_Success()
        {
            //Act
            var action = await warehouseController.CreatePart() as ViewResult;
            var model = action.Model as PostPutPartViewModel;

            //Assert
            Assert.Null(model.Part);
        }

        [Fact]
        public async Task CreatePart_PostAction_NotValidModel_Success()
        {
            //Arrange
            warehouseController.ModelState.AddModelError("", "");

            //Act
            var action = await warehouseController.CreatePart(part) as ViewResult;
            var model = action.Model as PostPutPartViewModel;

            //Assert
            Assert.Null(model.Part);
        }

        [Fact]
        public async Task CreatePart_PostAction_ValidModel_Success()
        {
            //Act
            var action = await warehouseController.CreatePart(part) as RedirectToActionResult;

            //Assert
            warehouseService.Verify(x => x.AddPart(part), Times.Once);
            Assert.Equal("Index", action.ActionName);
        }
    }
}