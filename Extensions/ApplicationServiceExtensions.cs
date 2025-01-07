using ApotekaBackend.Data;
using ApotekaBackend.Helpers;
using ApotekaBackend.Interfaces;
using ApotekaBackend.Repositories;
using ApotekaBackend.Services;
using Hangfire;
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
            services.AddScoped<IReceptDoctor,ReceptService>();
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            services.AddHangfire(config2 =>
          config2.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseDefaultTypeSerializer()
                .UseSqlServerStorage(config.GetConnectionString("DefaultConnection"))); // Or your specific storage provider

            services.AddHangfireServer();
            return services;

        }
    }
}
