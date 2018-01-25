using System;

namespace Orders.Infrastructure.Dtos
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get;  set; }
        public DateTime CreatedAt { get;  set; }
    }
}