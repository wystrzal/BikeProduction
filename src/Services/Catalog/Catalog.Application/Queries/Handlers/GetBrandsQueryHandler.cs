using AutoMapper;
using BikeBaseRepository;
using Catalog.Application.Mapping;
using Catalog.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Application.Queries.Handlers
{
    public class GetBrandsQueryHandler : IRequestHandler<GetBrandsQuery, List<GetBrandsDto>>
    {
        private readonly IBaseRepository<Brand> repository;
        private readonly IMapper mapper;

        public GetBrandsQueryHandler(IBaseRepository<Brand> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<GetBrandsDto>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
        {
            var brands = await repository.GetAll();

            return mapper.Map<List<GetBrandsDto>>(brands);
        }
    }
}
