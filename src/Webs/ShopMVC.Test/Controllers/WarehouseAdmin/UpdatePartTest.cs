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
    public class UpdatePartTest
    {
        private const int partId = 1;

        private readonly Mock<IWarehouseService> warehouseService;

        private readonly WarehouseController warehouseController;
        private readonly Part part;

        public UpdatePartTest()
        {
            warehouseService = new Mock<IWarehouseService>();
            warehouseController = new WarehouseController(warehouseService.Object);
            part = new Part { Id = partId };
        }

        [Fact]
        public async Task UpdatePart_Success()
        {
            //Arrange
            warehouseService.Setup(x => x.GetPart(partId)).Returns(Task.FromResult(part));

            //Act
            var action = await warehouseController.UpdatePart(partId) as ViewResult;
            var model = action.Model as PostPutPartViewModel;

            //Arrange
            Assert.Equal(partId, model.Part.Id);
        }

        [Fact]
        public async Task UpdatePart_PostAction_NotValidModel_Success()
        {
            //Arrange
            warehouseController.ModelState.AddModelError("", "");
            warehouseService.Setup(x => x.GetPart(partId)).Returns(Task.FromResult(part));

            //Act
            var action = await warehouseController.UpdatePart(part) as ViewResult;
            var model = action.Model as PostPutPartViewModel;

            //Arrange
            Assert.Equal(partId, model.Part.Id);
        }

        [Fact]
        public async Task UpdatePart_PostAction_ValidModel_Success()
        {
            //Act
            var action = await warehouseController.UpdatePart(part) as RedirectToActionResult;

            //Assert
            warehouseService.Verify(x => x.UpdatePart(part), Times.Once);
            Assert.Equal("Index", action.ActionName);
        }
    }
}