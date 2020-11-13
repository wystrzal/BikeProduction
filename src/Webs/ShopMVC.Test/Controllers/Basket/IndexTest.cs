using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopMVC.Controllers;
using ShopMVC.Interfaces;
using ShopMVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopMVC.Test.Controllers.Basket
{
    public class IndexTest
    {
        private readonly Mock<IBasketService> basketService;

        private readonly BasketController basketController;

        public IndexTest()
        {
            basketService = new Mock<IBasketService>();
            basketController = new BasketController(basketService.Object);
        }

        [Fact]
        public async Task Index_Success()
        {
            //Arrange
            basketService.Setup(x => x.GetBasket()).Returns(Task.FromResult((UserBasketViewModel)null));

            //Act
            var action = await basketController.Index() as ViewResult;
            var value = action.Model as UserBasketViewModel;

            //Assert
            Assert.NotNull(value);
            Assert.NotNull(value.Products);
        }
    }
}
