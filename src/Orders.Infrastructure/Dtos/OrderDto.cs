using System;
using System.Collections.Generic;
using Orders.Core.Domain;

namespace Orders.Infrastructure.Dtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<Item> Items { get; set; }
        public Status Status { get; set; }
    }
}