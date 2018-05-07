using System.Collections.Generic;
using AutoMapper;
using Orders.Core.Domain;
using Orders.Infrastructure.Dtos;
using Orders.Infrastructure.Mappings;

namespace Orders.Infrastructure.Services.Extensions
{
    public static class DtoFactory
    {
        private static readonly IMapper mapper = AutoMapperConfig.GetMapper();

        public static OrderDto Dto(this Order order)
        {
            return mapper.Map<OrderDto>(order);
        }

        public static IEnumerable<OrderDto> Dto(this IEnumerable<Order> orders)
        {
            return mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public static CategoryDto Dto(this Category category)
        {
            return mapper.Map<CategoryDto>(category);
        }

        public static IEnumerable<CategoryDto> Dto(this IEnumerable<Category> categories)
        {
            return mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public static ItemDto Dto(this Item item)
        {
            return mapper.Map<ItemDto>(item);
        }

        public static IEnumerable<ItemDto> Dto(this IEnumerable<Item> items)
        {
            return mapper.Map<IEnumerable<ItemDto>>(items);
        }

        public static UserDto Dto(this User user)
        {
            return mapper.Map<UserDto>(user);
        }

        public static IEnumerable<UserDto> Dto(this IEnumerable<User> users)
        {
            return mapper.Map<IEnumerable<UserDto>>(users);
        }
    }
}