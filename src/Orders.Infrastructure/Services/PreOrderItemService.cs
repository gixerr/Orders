using System;
using System.Threading.Tasks;
using Orders.Core.Repositories;
using Orders.Infrastructure.Repositories.Extensions;
using Orders.Infrastructure.Services.Interfaces;

namespace Orders.Infrastructure.Services
{
    public class PreOrderItemService : IPreOrderItemService
    {
        private readonly IItemRepository _itemRepository;

        private readonly IPreOrderService _preOrderService;

        public PreOrderItemService(IItemRepository itemRepository, IPreOrderService preOrderService)
        {
            _preOrderService = preOrderService;
            _itemRepository = itemRepository;
        }
        public async Task AddAsync(Guid itemId, string orderName)
        {
            var item =  await _itemRepository.GetOrFailAsync(itemId);
            var preOrder = _preOrderService.Get(orderName);
            preOrder.AddItem(item);
            _preOrderService.Update(preOrder);
        }
    }
}