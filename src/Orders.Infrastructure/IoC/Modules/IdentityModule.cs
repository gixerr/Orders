using Autofac;
using Microsoft.AspNetCore.Identity;
using Orders.Core.Domain;

namespace Orders.Infrastructure.IoC.Modules
{
    public class IdentityModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PasswordHasher<User>>()
                .As<IPasswordHasher<User>>();
        }
    }
}