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

        public IContainer Build()
        {
            _services.AddMvc();

            var builder = new ContainerBuilder();
            builder.Populate(_services);

            builder.RegisterModule(new ContainerModule(_configuration));
            
            return builder.Build();
        }
    }
}