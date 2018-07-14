using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Orders.Core.Domain;
using Orders.Infrastructure.MongoDb;
using Orders.Infrastructure.Options;
using Orders.Infrastructure.Policies;

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

        public static void LoadAuthenticationPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();

                options.AddPolicy(Policy.AdminOnly, cfg =>
                {
                    cfg.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                    cfg.RequireRole(Role.Admin.ToString());
                });
            });
        }

        public static void LoadOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtOptions>(configuration.GetSection("jwt"));
            services.Configure<MongoSettings>(configuration.GetSection("mongoDb"));
        }
    }
}