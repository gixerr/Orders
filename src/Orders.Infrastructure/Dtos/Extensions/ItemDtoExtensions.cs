using AutoMapper;
using Orders.Core.Domain;
using Orders.Infrastructure.Mappings;

namespace Orders.Infrastructure.Dtos.Extensions
{
    public static class ItemDtoExtensions
    {
        private static readonly IMapper Mapper = AutoMapperConfig.GetMapper();
        public static PreOrderItem ToPreOrderItem(this ItemDto itemDto)
            => Mapper.Map<PreOrderItem>(itemDto);
    }
}