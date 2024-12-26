using ApotekaBackend.Data;
using ApotekaBackend.Helpers;
using ApotekaBackend.Interfaces;
using ApotekaBackend.Repositories;
using ApotekaBackend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ApotekaBackend.Extensions
{
    public static class ApplicationServiceExtensions
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration config)
        {

            services.AddControllers();
           services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen();
            services.AddCors();
           services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ILekRepository, LekRepository>();    
            services.AddScoped<IKlijentRepository, KlijentRepository>();
            services.AddScoped<IReceptRepository, ReceptRepository>();  
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPhotoService, PhotoService>();    
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            return services;

        }
    }
}
