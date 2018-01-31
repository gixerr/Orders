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

            await _context.Resolve<ICommandHandler<T>>().HandleAsync(command);
        }
    }
}