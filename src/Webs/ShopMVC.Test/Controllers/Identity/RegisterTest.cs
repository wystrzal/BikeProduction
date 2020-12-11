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
    public class RegisterTest
    {
        private readonly Mock<IIdentityService> identityService;
        private readonly Mock<IBus> bus;

        private readonly IdentityController controller;

        public RegisterTest()
        {
            identityService = new Mock<IIdentityService>();
            bus = new Mock<IBus>();
            controller = new IdentityController(identityService.Object, bus.Object);
        }

        [Fact]
        public async Task Register_ModelIsNotValid()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            controller.ModelState.AddModelError("error", "error");
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            //Act
            var action = await controller.Register(It.IsAny<RegisterDto>()) as RedirectToActionResult;

            //Assert
            Assert.Equal("Index", action.ActionName);
            Assert.Equal("Home", action.ControllerName);
            Assert.True(controller.ModelState.ErrorCount > 0);
        }

        [Fact]
        public async Task Register_NullUserName()
        {
            //Arrange
            var registerDto = new RegisterDto { Password = "Test123", UserName = null };
            var httpContext = new DefaultHttpContext();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            //Act
            var action = await controller.Register(registerDto) as RedirectToActionResult;

            //Assert
            Assert.Equal("Index", action.ActionName);
            Assert.Equal("Home", action.ControllerName);
            Assert.True(controller.ModelState.ErrorCount > 0);
        }

        [Fact]
        public async Task Register_PasswordWithoutUppercase()
        {
            //Arrange
            var registerDto = new RegisterDto { Password = "test123", UserName = "test" };
            var httpContext = new DefaultHttpContext();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            //Act
            var action = await controller.Register(registerDto) as RedirectToActionResult;

            //Assert
            Assert.Equal("Index", action.ActionName);
            Assert.Equal("Home", action.ControllerName);
            Assert.True(controller.ModelState.ErrorCount > 0);
        }

        [Fact]
        public async Task Register_PasswordWithoutDigit()
        {
            //Arrange
            var registerDto = new RegisterDto { Password = "Test", UserName = "test" };
            var httpContext = new DefaultHttpContext();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            //Act
            var action = await controller.Register(registerDto) as RedirectToActionResult;

            //Assert
            Assert.Equal("Index", action.ActionName);
            Assert.Equal("Home", action.ControllerName);
            Assert.True(controller.ModelState.ErrorCount > 0);
        }

        [Fact]
        public async Task Register_Fail()
        {
            //Arrange
            var registerDto = new RegisterDto { Password = "Test123", UserName = "test" };
            var httpResponse = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized)
            {
                Content = Mock.Of<HttpContent>()
            };
            var httpContext = new DefaultHttpContext();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            identityService.Setup(x => x.Register(registerDto)).Returns(Task.FromResult(httpResponse));

            //Act
            var action = await controller.Register(registerDto) as RedirectToActionResult;

            //Assert
            Assert.Equal("Index", action.ActionName);
            Assert.Equal("Home", action.ControllerName);
            Assert.True(controller.ModelState.ErrorCount > 0);
        }

        [Fact]
        public async Task Register_Success()
        {
            //Arrange
            var registerDto = new RegisterDto { Password = "Test123", UserName = "test" };
            var httpResponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            identityService.Setup(x => x.Register(registerDto)).Returns(Task.FromResult(httpResponse));

            //Act
            var action = await controller.Register(registerDto) as RedirectToActionResult;

            //Assert
            Assert.Equal("Index", action.ActionName);
            Assert.Equal("Home", action.ControllerName);
            Assert.False(controller.ModelState.ErrorCount > 0);
        }
    }
}
