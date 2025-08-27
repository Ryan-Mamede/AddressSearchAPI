using AddressSearch.Services.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressSearch.Services.Configurations
{
    public static class JwtConfiguration
    {
        public static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration config)
        {
            var section = config.GetSection("JwtSettings");
            var settings = section.Get<JwtSettings>() ?? throw new InvalidOperationException("JwtSettings ausente");
            services.AddSingleton(settings);
            services.AddTransient<JwtService>();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.SecretKey));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.RequireHttpsMetadata = false;
                    opt.SaveToken = true;
                    opt.TokenValidationParameters = new()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateIssuer = !string.IsNullOrWhiteSpace(settings.Issuer),
                        ValidIssuer = settings.Issuer,
                        ValidateAudience = !string.IsNullOrWhiteSpace(settings.Audience),
                        ValidAudience = settings.Audience,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddAuthorization();
            return services;
        }
    }
}
