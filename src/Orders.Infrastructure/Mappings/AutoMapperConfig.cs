using System;
using AutoMapper;
using Orders.Core.Domain;
using Orders.Infrastructure.Commands.PreOrders;
using Orders.Infrastructure.Dtos;

namespace Orders.Infrastructure.Mappings
{
    public static class AutoMapperConfig
    {
        public static IMapper GetMapper()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Order, OrderDto>()
                    .ForMember(m => m.Status, o => o.MapFrom(s =>
                        (StatusDto)Enum.Parse(typeof(StatusDto), s.Status.ToString(), true)));

                cfg.CreateMap<Category, CategoryDto>();

                cfg.CreateMap<Item, ItemDto>();

                cfg.CreateMap<User, UserDto>()
                    .ForMember(m => m.Role, o => o.MapFrom(s => 
                        (RoleDto)Enum.Parse(typeof(RoleDto), s.Role.ToString(), true)));
            })
            .CreateMapper();
    }
}