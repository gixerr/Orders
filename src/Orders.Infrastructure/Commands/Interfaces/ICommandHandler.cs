using System.Threading.Tasks;

namespace Orders.Infrastructure.Commads.Interfaces
{
    public interface ICommandHandler<T> where T : ICommand
    {
         Task HandleAsync(T command);
    }
}