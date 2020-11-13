using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopMVC.Controllers;
using ShopMVC.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopMVC.Test.Controllers.Basket
{
    public class ClearBasketTest
    {
        private const string actionName = "Index";
        private const string controllerName = "Home";

        private readonly Mock<IBasketService> basketService;

        private readonly BasketController basketController;

        public ClearBasketTest()
        {
            basketService = new Mock<IBasketService>();
            basketController = new BasketController(basketService.Object);
        }

        [Fact]
        public async Task ClearBasket_Success()
        {
            //Act
            var action = await basketController.ClearBasket() as RedirectToActionResult;

            //Assert
            basketService.Verify(x => x.ClearBasket(), Times.Once);
            Assert.Equal(actionName, action.ActionName);
            Assert.Equal(controllerName, action.ControllerName);
        }
    }
}
