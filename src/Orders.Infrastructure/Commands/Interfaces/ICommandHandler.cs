using System.Threading.Tasks;

namespace Orders.Infrastructure.Commands.Interfaces
{
    public interface ICommandHandler<T> where T : ICommand
    {
        Task HandleAsync(T command);
    }

    public interface ICommandHandler<TCommand, TResult> where TCommand : ICommand
    {
        Task<TResult> HandleAsync(TCommand command);
    }
}