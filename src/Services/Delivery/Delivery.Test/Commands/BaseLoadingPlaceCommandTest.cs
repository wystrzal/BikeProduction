using Delivery.Application.Commands;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Delivery.Test.Commands
{
    public class BaseLoadingPlaceCommandTest
    {
        [Fact]
        public void BaseLoadingPlaceCommand_NullLoadingPlaceId_ThrowsArgumentException()
        {
            //Assert
            Assert.Throws<ArgumentException>(() => new DeleteLoadingPlaceCommand(It.IsAny<int>()));
        }
    }
}
