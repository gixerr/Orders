using AutoMapper;
using Orders.Core.Domain;
using Orders.Infrastructure.Mappings;

namespace Orders.Infrastructure.Dtos.Extensions
{
    public static class ItemDtoExtensions
    {
        private static readonly IMapper mapper = AutoMapperConfig.GetMapper();
        public static PreOrderItem ToPreOrderItem(this ItemDto itemDto)
            => mapper.Map<PreOrderItem>(itemDto);
    }
}