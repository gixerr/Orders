using System;
using System.Threading.Tasks;
using Autofac;
using Orders.Infrastructure.Commands.Interfaces;

namespace Orders.Infrastructure.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IComponentContext _context;

        public CommandDispatcher(IComponentContext context)
        {
            _context = context;
        }

        public async Task DispatchAsync<T>(T command) where T : ICommand
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command), $"Commad '{typeof(T).Name} cannot be null.");
            }

            var handler = _context.Resolve<ICommandHandler<T>>();

            if(handler is null)
            {
                throw new ArgumentNullException(nameof(command), $"Commad handler '{typeof(T).Name} not found.");
            }
            await handler.HandleAsync(command);
        }

        public async Task<TResult> DispatchAsync<TCommand, TResult>(TCommand command) where TCommand : ICommand
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command), $"Commad '{typeof(TCommand).Name} cannot be null.");
            }

            var handler = _context.Resolve<ICommandHandler<TCommand, TResult>>();

            if(handler is null)
            {
                throw new ArgumentNullException(nameof(command), $"Commad handler '{typeof(TCommand).Name} not found.");
            }

            return await handler.HandleAsync(command);
        }
    }
}