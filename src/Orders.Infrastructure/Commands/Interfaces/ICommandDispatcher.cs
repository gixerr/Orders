using System.Threading.Tasks;

namespace Orders.Infrastructure.Commads.Interfaces
{
    public interface ICommandDispatcher
    {
         Task DispatchAsync<T>(T command) where T : ICommand;
    }
}