using AutoMapper;
using Delivery.Application.Mapping;
using Delivery.Application.Queries;
using Delivery.Application.Queries.Handlers;
using Delivery.Core.Interfaces;
using Delivery.Core.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Delivery.Test.Queries
{
    public class GetLoadingPlaceQueryHandlerTest
    {
        private const int loadingPlaceId = 1;

        private readonly Mock<ILoadingPlaceRepo> loadingPlaceRepo;
        private readonly Mock<IMapper> mapper;

        private readonly GetLoadingPlaceQuery query;
        private readonly GetLoadingPlaceQueryHandler queryHandler;
        private readonly LoadingPlace loadingPlace;
        private readonly GetLoadingPlaceDto loadingPlaceDto;

        public GetLoadingPlaceQueryHandlerTest()
        {
            loadingPlaceRepo = new Mock<ILoadingPlaceRepo>();
            mapper = new Mock<IMapper>();
            query = new GetLoadingPlaceQuery(loadingPlaceId);
            queryHandler = new GetLoadingPlaceQueryHandler(loadingPlaceRepo.Object, mapper.Object);
            loadingPlace = new LoadingPlace { Id = loadingPlaceId };
            loadingPlaceDto = new GetLoadingPlaceDto { Id = loadingPlaceId };
        }

        [Fact]
        public async Task GetLoadingPlaceQueryHandler_Success()
        {
            //Arrange
            loadingPlaceRepo.Setup(x => x.GetByConditionWithIncludeFirst(
                It.IsAny<Func<LoadingPlace, bool>>(), It.IsAny<Expression<Func<LoadingPlace, ICollection<PackToDelivery>>>>()))
                .Returns(Task.FromResult(loadingPlace));

            mapper.Setup(x => x.Map<GetLoadingPlaceDto>(loadingPlace)).Returns(loadingPlaceDto);

            //Act
            var action = await queryHandler.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(loadingPlaceId, action.Id);
        }

        [Fact]
        public void GetLoadingPlaceQueryHandler_ThrowsArgumentException()
        {
            //Assert
            Assert.Throws<ArgumentException>(() => new GetLoadingPlaceQuery(It.IsAny<int>()));
        }
    }
}
