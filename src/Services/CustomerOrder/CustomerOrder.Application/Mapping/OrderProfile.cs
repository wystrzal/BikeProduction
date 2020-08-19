using AutoMapper;
using CustomerOrder.Application.Commands;
using CustomerOrder.Core.Models;

namespace CustomerOrder.Application.Mapping
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<CreateOrderCommand, Order>();
            CreateMap<Order, GetOrderDto>();
            CreateMap<Order, GetOrdersDto>();
        }
    }
}
