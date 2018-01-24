using System.Threading.Tasks;

namespace Orders.Infrastructure.Commands.Interfaces
{
    public interface ICommandHandler<T> where T : ICommand
    {
         Task HandleAsync(T command);
    }
}