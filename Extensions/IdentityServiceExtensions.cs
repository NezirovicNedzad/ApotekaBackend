using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ApotekaBackend.Extensions
{
    public static class IdentityServiceExtensions
    {


        public static IServiceCollection AddIdentityServices(this IServiceCollection services,
            IConfiguration config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
                    {
                        var tokeKey = config["TokenKey"] ?? throw new Exception("TokenKey was not found");
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokeKey)),
                            ValidateIssuer = false,
                            ValidateAudience = false,
                        };

                    });

            return services;
        }
    }
}
