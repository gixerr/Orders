using System.Collections.Immutable;
using Autofac;
using Microsoft.Extensions.Configuration;
using Orders.Infrastructure.MongoDb;
using Orders.Infrastructure.Settings;

namespace Orders.Infrastructure.IoC.Modules
{
    public class SettingsModule : Module
    {
        private readonly IConfiguration _configuration;

        public SettingsModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var mongoSettings = new MongoSettings();
            _configuration.GetSection("mongo").Bind(mongoSettings);
            builder.RegisterInstance(mongoSettings).SingleInstance();

            var dataSettings = new DataSettings();
            _configuration.GetSection("data").Bind(dataSettings);
            builder.RegisterInstance(dataSettings).SingleInstance();
        }
    }
}