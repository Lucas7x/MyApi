using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyWallet.Infrastructure.Database;

namespace MyApi.Extensions
{
    public static class BuilderServiceExtensions
    {
        public static IServiceCollection ConfigureDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            string? defaultConnection = configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrWhiteSpace(defaultConnection))
                throw new ArgumentNullException(nameof(defaultConnection));

            services.AddDbContext<DataContext>(
                options => options.UseSqlite(defaultConnection)
            );

            return services;
        }

        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes( configuration["Jwt:IssuerSigningKey"]! )),
                    ClockSkew = TimeSpan.Zero
                };
            });

            return services;
        }
    }
}
