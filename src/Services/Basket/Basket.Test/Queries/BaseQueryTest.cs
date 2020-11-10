using Basket.Application.Queries;
using Moq;
using System;
using Xunit;

namespace Basket.Test.Queries
{
    public class BaseQueryTest
    {
        [Fact]
        public void BaseQuery_NullUserId_ThrowsArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new GetBasketQuery(It.IsAny<string>()));
        }
    }
}
