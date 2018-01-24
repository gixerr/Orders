using System.Threading.Tasks;

namespace Orders.Infrastructure.Commands.Interfaces
{
    public interface ICommandDispatcher
    {
         Task DispatchAsync<T>(T command) where T : ICommand;
    }
}