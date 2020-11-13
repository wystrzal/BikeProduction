using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using ShopMVC.Controllers;
using ShopMVC.Interfaces;
using ShopMVC.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopMVC.Test.Controllers.Basket
{
    public class AddProductTest
    {
        private const int quantity = 1;

        private readonly Mock<IBasketService> basketService;

        private readonly BasketController basketController;
        private readonly BasketProduct basketProduct;

        public AddProductTest()
        {
            basketService = new Mock<IBasketService>();
            basketController = new BasketController(basketService.Object);
            basketProduct = new BasketProduct();
        }

        [Fact]
        public async Task AddProduct_Success()
        {
            //Arrange
            basketService.Setup(x => x.GetBasketQuantity()).Returns(Task.FromResult(quantity));

            //Act
            var action = await basketController.AddProduct(basketProduct) as JsonResult;

            //Assert
            basketService.Verify(x => x.AddProduct(basketProduct), Times.Once);
            Assert.Contains(quantity.ToString(), action.Value.ToString());
        }
    }
}
