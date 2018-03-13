using Autofac;
using Orders.Infrastructure.Commands;
using Orders.Infrastructure.Commands.Interfaces;

namespace Orders.Infrastructure.IoC.Modules
{
    public class CommandModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(CommandModule).Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(ICommandHandler<>))
                .InstancePerLifetimeScope();
            
             builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(ICommandHandler<,>))
                .InstancePerLifetimeScope();

            builder.RegisterType<CommandDispatcher>()
                .As<ICommandDispatcher>()
                .InstancePerLifetimeScope();


        }
    }
}