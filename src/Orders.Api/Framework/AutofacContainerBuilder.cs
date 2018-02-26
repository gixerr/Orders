using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Orders.Core.Repositories;
using Orders.Infrastructure.IoC.Modules;
using Orders.Infrastructure.Mappings;
using Orders.Infrastructure.Repositories;
using Orders.Infrastructure.Services;
using Orders.Infrastructure.Services.Interfaces;

namespace Orders.Api.Framework
{
    public class AutofacContainerBuilder
    {
        private readonly IServiceCollection _services;

        public AutofacContainerBuilder(IServiceCollection services)
        {
            _services = services;
        }

        public IContainer Build()
        {
            _services.AddMvc();

            var builder = new ContainerBuilder();
            builder.Populate(_services);

            builder.RegisterType<InMemoryOrderRepository>().As<IOrderRepository>().SingleInstance();
            builder.RegisterType<InMemoryCategoryRepository>().As<ICategoryRepository>().SingleInstance();
            builder.RegisterType<InMemoryItemRepository>().As<IItemRepository>().SingleInstance();
            builder.RegisterType<OrderService>().As<IOrderService>().InstancePerLifetimeScope();
            builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerLifetimeScope();
            builder.RegisterType<ItemService>().As<IItemService>().InstancePerLifetimeScope();
            builder.RegisterModule<CommandModule>();

            return builder.Build();
        }
    }
}