using Orders.Infrastructure.Commands.Interfaces;

namespace Orders.Infrastructure.Commands.Items
{
    public class AddItem : ICommand
    {
        public string ItemName { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
    }
}