using Moq;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace BikeExtensions
{
    public static class MockLoggerExtensions
    {
        public static void VerifyLogging<T>(this Mock<ILogger<T>> logger, LogLevel logLevel)
        {
            logger.Verify(x => 
                x.Log(It.Is<LogLevel>(x => x == logLevel),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((x, y) => true),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Once);
        }
    }
}
