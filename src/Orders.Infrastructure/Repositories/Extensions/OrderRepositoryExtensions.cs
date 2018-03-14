using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orders.Core.Domain;
using Orders.Core.Exceptions;
using Orders.Core.Repositories;
using Orders.Infrastructure.Dtos;
using Orders.Infrastructure.Exceptions;

namespace Orders.Infrastructure.Repositories.Extensions
{
    public static class OrderRepositoryExtensions
    {
        public static async Task<IEnumerable<Order>> GetAllOrFailAsync(this IOrderRepository orderRepository)
        {
            var orders = await orderRepository.GetAllAsync();
            if (orders is null)
            {
                throw new ServiceException(ErrorCode.order_not_found, "No orders available.");
            }

            return orders;
        }
        public static async Task<Order> GetOrFailAsync(this IOrderRepository orderRepository, Guid id)
        {
            var order = await orderRepository.GetAsync(id);
            if (order is null)
            {
                throw new ServiceException(ErrorCode.order_not_found, $"Order with given id '{id}' not found.");
            }

            return order;
        }

        public static async Task<Order> GetOrFailAsync(this IOrderRepository orderRepository, string name)
        {
            var order = await orderRepository.GetAsync(name);
            if (order is null)
            {
                throw new ServiceException(ErrorCode.order_not_found, $"Order with given name '{name}' not found.");
            }

            return order;
        }

        public static async Task<IEnumerable<Order>> GetOrFailAsync(this IOrderRepository orderRepository,
            StatusDto statusDto)
        {
            var status = (Status)Enum.Parse(typeof(Status), statusDto.ToString(), true);
            var orders = await orderRepository.GetAsync(status);
            if (orders is null)
            {
                throw new ServiceException(ErrorCode.order_not_found, $"Order with given status '{status}' not found.");
            }

            return orders;
        }

        public static async Task AddOrFailAsync(this IOrderRepository orderRepository, Guid id, string name)
        {
            var order = await orderRepository.GetAsync(name);
            if (!(order is null))
            {
                throw new ServiceException(ErrorCode.order_already_exists, $"Order with given name '{name}' already exist. Order name must be unique.");
            }
            order = new Order(id, name);
            await orderRepository.AddAsync(order);
        }

        public static async Task RemoveOrFailAsync(this IOrderRepository orderRepository, Guid id)
        {
            var order = await orderRepository.GetAsync(id);
            if (order is null)
            {
                throw new ServiceException(ErrorCode.order_not_found, $"Order with given id '{id}' not found. Unable to remove nonexiting order.");
            }
            await orderRepository.RemoveAsync(id);
        }
    }
}