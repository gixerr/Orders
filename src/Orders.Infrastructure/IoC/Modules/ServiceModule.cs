using Autofac;
using Orders.Infrastructure.Services.Interfaces;

namespace Orders.Infrastructure.IoC.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(ServiceModule).Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.IsAssignableTo<IService>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}