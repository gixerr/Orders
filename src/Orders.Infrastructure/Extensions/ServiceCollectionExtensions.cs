using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Orders.Infrastructure.Options;

namespace Orders.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddJwt(this IServiceCollection services)
        {
            IConfiguration configuration;
            using(var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }
            var section = configuration.GetSection("jwt");
            var options = new JwtOptions();
            section.Bind(options);
            services.AddAuthentication()
                .AddJwtBearer(cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey)),
                        ValidIssuer = options.Issuer,
                        ValidateAudience = false,
                        ValidateLifetime = true
                    };
                });
        

        }
    }
}