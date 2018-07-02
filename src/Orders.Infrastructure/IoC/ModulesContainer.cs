using Autofac;
using Microsoft.Extensions.Configuration;
using Orders.Infrastructure.IoC.Modules;
using Orders.Infrastructure.Services.Interfaces;

namespace Orders.Infrastructure.IoC
{
    public class ModulesContainer : Autofac.Module
    {
        private readonly IConfiguration _configuration;

        public ModulesContainer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<CommandModule>();
            builder.RegisterModule<RepositoryModule>();
            builder.RegisterModule<ServiceModule>();
            builder.RegisterModule<IdentityModule>();
        }
    }
}
