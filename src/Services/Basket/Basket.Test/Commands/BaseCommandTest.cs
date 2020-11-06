using Basket.Application.Commands;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Basket.Test.Commands
{
    public class BaseCommandTest
    {
        private const int id = 1;

        [Fact]
        public void BaseCommand_NullUserId_ThrowsArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new RemoveProductCommand(It.IsAny<string>(), id));
        }
    }
}
