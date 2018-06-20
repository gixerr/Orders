using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Orders.Core.Domain;
using Orders.Infrastructure.Extensions;
using Orders.Infrastructure.Options;

namespace Orders.Infrastructure.IoC
{
    public class ServicesContainer
    {
        private readonly IServiceCollection _services;
        private readonly IConfiguration _configuration;

        public ServicesContainer(IServiceCollection services, IConfiguration configuration)
        {
            _services = services;
            _configuration = configuration;

        }
        public IServiceCollection Load()
        {
            _services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.Formatting = Formatting.Indented;
                });
            _services.AddMemoryCache();
            _services.AddJwt();
            _services.AddAuthorization(options => 
                { 
                    options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                        .RequireAuthenticatedUser()
                        .RequireRole(Role.Admin.ToString())
                        .Build(); 
                    
                    options.AddPolicy("Test", policyCfg => policyCfg.RequireAuthenticatedUser());
                });

            _services.Configure<JwtOptions>(_configuration.GetSection("jwt"));

            return _services;
        }
    }
}