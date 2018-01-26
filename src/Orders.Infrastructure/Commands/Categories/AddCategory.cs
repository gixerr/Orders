namespace Orders.Infrastructure.Commands.Categories
{
    public class AddCategory : ICommand
    {
        public string Name { get; set; }
    }
}