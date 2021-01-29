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
    public class IndexTest
    {
        private const int productId = 1;
        private const string reference = "1";

        private readonly Mock<IWarehouseService> warehouseService;

        private readonly WarehouseController warehouseController;

        public IndexTest()
        {
            warehouseService = new Mock<IWarehouseService>();
            warehouseController = new WarehouseController(warehouseService.Object);
        }

        [Fact]
        public async Task Index_Success()
        {
            //Arrange
            var parts = new List<Part> { new Part(), new Part() };
            warehouseService.Setup(x => x.GetParts()).Returns(Task.FromResult(parts));

            //Act
            var action = await warehouseController.Index(reference, productId) as ViewResult;
            var model = action.Model as PartsViewModel;

            //Assert
            Assert.Equal(reference, model.Reference);
            Assert.Equal(productId, model.ProductId);
            Assert.Equal(parts.Count, model.Parts.Count);
        }
    }
}