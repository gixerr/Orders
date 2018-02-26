using System.Collections.Generic;
using AutoMapper;
using Orders.Core.Domain;
using Orders.Infrastructure.Dtos;
using Orders.Infrastructure.Mappings;

namespace Orders.Infrastructure.Services.Extensions
{
    public static class DtoFactory
    {
        private static readonly IMapper Mapper = AutoMapperConfig.GetMapper();

        public static OrderDto Dto(this Order order)
        {
            return Mapper.Map<OrderDto>(order);
        }

        public static IEnumerable<OrderDto> Dto(this IEnumerable<Order> orders)
        {
            return Mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public static CategoryDto Dto(this Category category)
        {
            return Mapper.Map<CategoryDto>(category);
        }

        public static IEnumerable<CategoryDto> Dto(this IEnumerable<Category> categories)
        {
            return Mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public static ItemDto Dto(this Item item)
        {
            return Mapper.Map<ItemDto>(item);
        }

        public static IEnumerable<ItemDto> Dto(this IEnumerable<Item> items)
        {
            return Mapper.Map<IEnumerable<ItemDto>>(items);
        }
    }
}