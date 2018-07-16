using Autofac;
using MongoDB.Driver;
using Orders.Core.Repositories;
using Orders.Infrastructure.MongoDb;

namespace Orders.Infrastructure.IoC.Modules
{
    public class MongoModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register((c, p) =>
            {
                var settings = c.Resolve<MongoSettings>();

                return new MongoClient(settings.ConnectionString);
            }).SingleInstance();

            builder.Register((c, p) =>
            {
                var clinet = c.Resolve<MongoClient>();
                var settings = c.Resolve<MongoSettings>();
                var database = clinet.GetDatabase(settings.DatabaseName);

                return database;
            }).As<IMongoDatabase>();
            
            var assembly = typeof(MongoModule).Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.IsAssignableTo<IMongoRepository>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}