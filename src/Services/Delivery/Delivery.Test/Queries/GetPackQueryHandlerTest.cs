using AutoMapper;
using Delivery.Application.Mapping;
using Delivery.Application.Queries;
using Delivery.Application.Queries.Handlers;
using Delivery.Core.Interfaces;
using Delivery.Core.Models;
using Moq;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Delivery.Test.Queries
{
    public class GetPackQueryHandlerTest
    {
        private const int packId = 1;

        private readonly Mock<IPackToDeliveryRepo> packToDeliveryRepo;
        private readonly Mock<IMapper> mapper;

        private readonly GetPackQuery query;
        private readonly GetPackQueryHandler queryHandler;
        private readonly PackToDelivery packToDelivery;
        private readonly GetPackDto packDto;

        public GetPackQueryHandlerTest()
        {
            packToDeliveryRepo = new Mock<IPackToDeliveryRepo>();
            mapper = new Mock<IMapper>();
            query = new GetPackQuery(packId);
            queryHandler = new GetPackQueryHandler(packToDeliveryRepo.Object, mapper.Object);
            packToDelivery = new PackToDelivery { Id = packId };
            packDto = new GetPackDto { Id = packId };
        }

        [Fact]
        public async Task GetPackQueryHandler_Success()
        {
            //Arrange
            packToDeliveryRepo.Setup(x => x.GetByConditionWithIncludeFirst(
                It.IsAny<Func<PackToDelivery, bool>>(), It.IsAny<Expression<Func<PackToDelivery, LoadingPlace>>>()))
                .Returns(Task.FromResult(packToDelivery));

            mapper.Setup(x => x.Map<GetPackDto>(packToDelivery)).Returns(packDto);

            //Act
            var action = await queryHandler.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(packId, action.Id);
        }

        [Fact]
        public void GetPackQueryHandler_ThrowsArgumentException()
        {
            //Assert
            Assert.Throws<ArgumentException>(() => new GetPackQuery(It.IsAny<int>()));
        }
    }
}
