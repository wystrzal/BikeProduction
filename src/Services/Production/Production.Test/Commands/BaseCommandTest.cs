using Moq;
using Production.Application.Commands;
using System;
using Xunit;

namespace Production.Test.Commands
{
    public class BaseCommandTest
    {
        [Fact]
        public void BaseCommand_NullProductionQueueId_ThrowsArgumentException()
        {
            //Assert
            Assert.Throws<ArgumentException>(() => new ConfirmProductionCommand(It.IsAny<int>()));
        }
    }
}
