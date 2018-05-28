using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Orders.Core.Domain;
using Orders.Core.Repositories;
using Orders.Infrastructure.Mappings;
using Orders.Infrastructure.Repositories.Extensions;
using Orders.Infrastructure.Services.Interfaces;

namespace Orders.Infrastructure.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderRepository _orderRepository;

        private readonly IItemService _itemService;

        private readonly IMapper _mapper;

        public OrderItemService(IOrderRepository orderRepository, IItemService itemService)
        {
            _itemService = itemService;
            _orderRepository = orderRepository;
            _mapper = AutoMapperConfig.GetMapper();
        }
        public async Task AddAsync(Guid orderId, Guid itemId)
        {
            var order = await _orderRepository.GetOrFailAsync(orderId);
            var orderItem = order.Items.SingleOrDefault(i => i.Id == itemId);
            if (orderItem is null)
            {
                var itemDto = await _itemService.GetAsync(itemId);
                orderItem = _mapper.Map<OrderItem>(itemDto); //TODO: Implement constructor
                order.AddOrderItem(orderItem);
                return;
            }
            orderItem.Counter.Increase();
        }

        public Task RemoveAsync(Guid orderId, Guid itemId)
        {
            throw new NotImplementedException();
        }
    }
}