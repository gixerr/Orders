using System;
using Orders.Core.Domain;

namespace Orders.Infrastructure.Dtos
{
    public class ItemDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public Counter Counter { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}