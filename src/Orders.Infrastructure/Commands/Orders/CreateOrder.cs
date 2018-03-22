using Orders.Infrastructure.Commands.Interfaces;

namespace Orders.Infrastructure.Commands.Orders
{
    public class CreateOrder : ICommand
    {
        public string Name { get; set; }
    }
}