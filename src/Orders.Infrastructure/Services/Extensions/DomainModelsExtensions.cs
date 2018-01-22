using System.Collections.Generic;
using AutoMapper;
using Orders.Core.Domain;
using Orders.Infrastructure.Dtos;

namespace Orders.Infrastructure.Services.Extensions
{
    public static class DomainModelsExtensions
    {
        public static OrderDto Dto(this Order order, IMapper mapper)
        {
            return mapper.Map<OrderDto>(order);
        }

        public static IEnumerable<OrderDto> Dto(this IEnumerable<Order> orders, IMapper mapper)
        {
            return mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public static CategoryDto Dto(this Category category, IMapper mapper)
        {
            return mapper.Map<CategoryDto>(category);
        }

        public static IEnumerable<CategoryDto> Dto(this IEnumerable<Category> categories, IMapper mapper)
        {
            return mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public static ItemDto Dto(this Item item, IMapper mapper)
        {
            return mapper.Map<ItemDto>(item);
        }

        public static IEnumerable<ItemDto> Dto(this IEnumerable<Item> items, IMapper mapper)
        {
            return mapper.Map<IEnumerable<ItemDto>>(items);
        }
    }
}