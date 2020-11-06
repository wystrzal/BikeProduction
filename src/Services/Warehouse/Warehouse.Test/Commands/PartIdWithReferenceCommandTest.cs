using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Application.Commands;
using Xunit;

namespace Warehouse.Test.Commands
{
    public class PartIdWithReferenceCommandTest
    {
        private const int partId = 1;
        private const string reference = "1";

        [Fact]
        public void PartIdWithReferenceCommand_PartIdEqualZero_ThrowsArgumentException()
        {
            //Assert
            Assert.Throws<ArgumentException>(() => new DeleteProductPartCommand(It.IsAny<int>(), reference));
        }

        [Fact]
        public void PartIdWithReferenceCommand_NullReference_ThrowsArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new DeleteProductPartCommand(partId, It.IsAny<string>()));
        }
    }
}
