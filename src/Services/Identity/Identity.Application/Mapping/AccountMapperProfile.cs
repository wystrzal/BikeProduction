using AutoMapper;
using Identity.Application.Commands;
using Identity.Core.Models;

namespace Identity.Application.Mapping
{
    public class AccountMapperProfile : Profile
    {
        public AccountMapperProfile()
        {
            CreateMap<RegisterCommand, User>();
        }
    }
}
