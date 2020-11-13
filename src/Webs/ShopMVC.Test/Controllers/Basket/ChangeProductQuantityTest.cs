using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopMVC.Controllers;
using ShopMVC.Interfaces;
using ShopMVC.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopMVC.Test.Controllers.Basket
{
    public class ChangeProductQuantityTest
    {
        private const string actionName = "Index";

        private readonly Mock<IBasketService> basketService;

        private readonly BasketController basketController;
        private readonly ChangeBasketProductQuantityDto dto;

        public ChangeProductQuantityTest()
        {
            basketService = new Mock<IBasketService>();
            basketController = new BasketController(basketService.Object);
            dto = new ChangeBasketProductQuantityDto();
        }

        [Fact]
        public async Task ChangeProductQuantity_Success()
        {
            //Act
            var action = await basketController.ChangeProductQuantity(dto) as RedirectToActionResult;

            //Assert
            basketService.Verify(x => x.ChangeProductQuantity(dto), Times.Once);
            Assert.Equal(actionName, action.ActionName);
        }
    }
}
