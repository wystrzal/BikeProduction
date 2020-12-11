using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using ShopMVC.Controllers;
using ShopMVC.Interfaces;
using ShopMVC.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopMVC.Test.Controllers.Identity
{
    public class LoginTest
    {
        private readonly Mock<IIdentityService> identityService;
        private readonly Mock<IBus> bus;

        private readonly IdentityController controller;

        public LoginTest()
        {
            identityService = new Mock<IIdentityService>();
            bus = new Mock<IBus>();
            controller = new IdentityController(identityService.Object, bus.Object);
        }

        [Fact]
        public async Task Login_ModelIsNotValid()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            controller.ModelState.AddModelError("error", "error");
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>()); 

            //Act
            var action = await controller.Login(It.IsAny<LoginDto>()) as RedirectToActionResult;

            //Assert
            Assert.Equal("Index", action.ActionName);
            Assert.Equal("Home", action.ControllerName);
            Assert.True(controller.ModelState.ErrorCount > 0);
        }

        [Fact]
        public async Task Login_Fail()
        {
            //Arrange
            var httpResponse = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            var httpContext = new DefaultHttpContext();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            identityService.Setup(x => x.Login(It.IsAny<LoginDto>())).Returns(Task.FromResult(httpResponse));

            //Act
            var action = await controller.Login(It.IsAny<LoginDto>()) as RedirectToActionResult;

            //Assert
            Assert.Equal("Index", action.ActionName);
            Assert.Equal("Home", action.ControllerName);
            Assert.True(controller.ModelState.ErrorCount > 0);
        }

        [Fact]
        public async Task Login_Success()
        {
            //Arrange
            var httpResponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            identityService.Setup(x => x.Login(It.IsAny<LoginDto>())).Returns(Task.FromResult(httpResponse));

            //Act
            var action = await controller.Login(It.IsAny<LoginDto>()) as RedirectToActionResult;

            //Assert
            Assert.Equal("Index", action.ActionName);
            Assert.Equal("Home", action.ControllerName);
            Assert.False(controller.ModelState.ErrorCount > 0);
        }
    }
}
