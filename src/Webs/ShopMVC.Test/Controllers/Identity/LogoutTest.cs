using Common.Application.Messaging;
using MassTransit;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Moq;
using ShopMVC.Controllers;
using ShopMVC.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ShopMVC.Test.Controllers.Identity
{
    public class LogoutTest
    {
        private readonly Mock<IIdentityService> identityService;
        private readonly Mock<IBus> bus;
        private readonly Mock<IServiceProvider> provider;

        private readonly IdentityController controller;

        public LogoutTest()
        {
            identityService = new Mock<IIdentityService>();
            bus = new Mock<IBus>();
            provider = new Mock<IServiceProvider>();
            controller = new IdentityController(identityService.Object, bus.Object);
        }

        [Fact]
        public async Task Logout_Success()
        {
            //Arrange
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            controller.ControllerContext.HttpContext.Session = Mock.Of<ISession>();
            controller.ControllerContext.HttpContext.RequestServices = provider.Object;

            provider.Setup(x => x.GetService(typeof(IAuthenticationService))).Returns(Mock.Of<IAuthenticationService>());
            provider.Setup(x => x.GetService(typeof(IUrlHelperFactory))).Returns(Mock.Of<IUrlHelperFactory>());

            //Act
            var action = await controller.Logout() as RedirectToActionResult;

            //Assert
            bus.Verify(x => x.Publish(It.IsAny<LoggedOutEvent>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal("Index", action.ActionName);
            Assert.Equal("Home", action.ControllerName);
        }
    }
}
