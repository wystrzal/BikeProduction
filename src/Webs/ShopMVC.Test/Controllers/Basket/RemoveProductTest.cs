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
    public class RemoveProductTest
    {
        private const string actionName = "Index";

        private readonly Mock<IBasketService> basketService;

        private readonly BasketController basketController;

        public RemoveProductTest()
        {
            basketService = new Mock<IBasketService>();
            basketController = new BasketController(basketService.Object);
        }

        [Fact]
        public async Task RemoveProduct_Success()
        {
            //Act
            var action = await basketController.RemoveProduct(It.IsAny<int>()) as RedirectToActionResult;

            //Assert
            basketService.Verify(x => x.RemoveProduct(It.IsAny<int>()), Times.Once);
            Assert.Equal(actionName, action.ActionName);
        }
    }
}
