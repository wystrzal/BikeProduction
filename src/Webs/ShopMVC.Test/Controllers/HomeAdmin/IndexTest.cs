using Microsoft.AspNetCore.Mvc;
using ShopMVC.Areas.Admin.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopMVC.Test.Controllers.HomeAdmin
{
    public class IndexTest
    {
        private readonly HomeController homeController;

        public IndexTest()
        {
            homeController = new HomeController();
        }

        [Fact]
        public void Index_Success()
        {
            //Act
            var action = homeController.Index() as ViewResult;

            //Assert
            Assert.NotNull(action);
        }
    }
}
