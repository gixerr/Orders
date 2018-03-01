using Autofac;
using Orders.Core.Repositories;

namespace Orders.Infrastructure.IoC.Modules
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(RepositoryModule).Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.IsAssignableTo<IRepository>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}