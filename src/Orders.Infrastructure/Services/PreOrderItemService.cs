using System;
using System.Linq;
using System.Threading.Tasks;
using Orders.Core.Domain.Extensions;
using Orders.Core.Repositories;
using Orders.Infrastructure.Dtos.Extensions;
using Orders.Infrastructure.Repositories.Extensions;
using Orders.Infrastructure.Services.Interfaces;

namespace Orders.Infrastructure.Services
{
    public class PreOrderItemService : IPreOrderItemService
    {
        private readonly IItemService _itemService;

        private readonly IPreOrderService _preOrderService;

        public PreOrderItemService(IItemService itemservice, IPreOrderService preOrderService)
        {
            _preOrderService = preOrderService;
            _itemService = itemservice;
        }
        
        public async Task AddAsync(Guid itemId, string orderName)
        {
            var itemDto = await _itemService.GetAsync(itemId);
            var preOrder = _preOrderService.Get(orderName);
            var containedItem = preOrder.Items.GetItem(itemId);
            if (!(containedItem is null))
            {
                containedItem.Counter.Increase();
                return;
            }
            var preOrderItem = itemDto.ToPreOrderItem();
            preOrder.AddItem(preOrderItem);
            _preOrderService.Update(preOrder);
        }

        public void Remove(Guid itemId, string orderName)
        {
            var preOrder = _preOrderService.Get(orderName);
            preOrder.RemoveItem(itemId);
            _preOrderService.Update(preOrder);
        }
    }
}