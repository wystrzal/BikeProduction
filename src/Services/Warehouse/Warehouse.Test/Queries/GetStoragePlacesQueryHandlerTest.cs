using AutoMapper;
using BikeBaseRepository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Warehouse.Application.Mapping;
using Warehouse.Application.Queries;
using Warehouse.Application.Queries.Handlers;
using Warehouse.Core.Interfaces;
using Warehouse.Core.Models;
using Warehouse.Infrastructure.Repositories;
using Xunit;

namespace Warehouse.Test.Queries
{
    public class GetStoragePlacesQueryHandlerTest
    {
        private readonly Mock<IStoragePlaceRepo> storagePlaceRepo;
        private readonly Mock<IMapper> mapper;

        public GetStoragePlacesQueryHandlerTest()
        {
            storagePlaceRepo = new Mock<IStoragePlaceRepo>();
            mapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task GetStoragePlacesQueryHandler_Success()
        {
            //Arrange
            IEnumerable<StoragePlace> storagePlaces = new List<StoragePlace> { new StoragePlace(), new StoragePlace() };
            var storagePlacesDto = new List<GetStoragePlacesDto> { new GetStoragePlacesDto(), new GetStoragePlacesDto() };

            storagePlaceRepo.Setup(x => x.GetStoragePlaces()).Returns(Task.FromResult(storagePlaces));
            mapper.Setup(x => x.Map<IEnumerable<GetStoragePlacesDto>>(storagePlaces)).Returns(storagePlacesDto);

            var queryHandler = new GetStoragePlacesQueryHandler(storagePlaceRepo.Object, mapper.Object);

            //Act
            var action = await queryHandler.Handle(It.IsAny<GetStoragePlacesQuery>(), It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(2, action.Count());
        }
    }
}
