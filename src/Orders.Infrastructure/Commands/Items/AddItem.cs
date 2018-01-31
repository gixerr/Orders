namespace Orders.Infrastructure.Commands.Items
{
    public class AddItem : ICommand
    {
        public string itemName { get; set; }
        public string categoryName { get; set; }
        public decimal price { get; set; }
    }
}