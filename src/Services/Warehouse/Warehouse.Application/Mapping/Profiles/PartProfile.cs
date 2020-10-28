using AutoMapper;
using Warehouse.Application.Commands;
using Warehouse.Core.Models;

namespace Warehouse.Application.Mapping
{
    public class PartProfile : Profile
    {
        public PartProfile()
        {
            CreateMap<AddPartCommand, Part>();

            CreateMap<Part, GetPartDto>();

            CreateMap<Part, GetPartsDto>();

            CreateMap<UpdatePartCommand, Part>();
        }
    }
}
