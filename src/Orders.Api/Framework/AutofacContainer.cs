using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orders.Infrastructure.IoC;

namespace Orders.Api.Framework
{
    public class AutofacContainer
    {
        private readonly IServiceCollection _services;
        private readonly IConfiguration _configuration;

        public AutofacContainer(IServiceCollection services, IConfiguration configuration)
        {
            _services = services;
            _configuration = configuration;
        }

        private IServiceCollection Services()
        {
            _services.AddMvc();

            return _services;
        }

        public IContainer Build()
        {
            var builder = new ContainerBuilder();
            builder.Populate(Services());

            builder.RegisterModule(new ContainerModule(_configuration));
            
            return builder.Build();
        }
    }
}