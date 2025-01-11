using ApotekaBackend.Data;
using ApotekaBackend.Extensions;
using ApotekaBackend.Interfaces;
using ApotekaBackend.MIddleware;
using ApotekaBackend.Models;
using ApotekaBackend.Services;
using ApotekaBackend.SignalR;
using FitnessBackend;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);    

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(x =>
    x.AllowAnyHeader()
     .AllowAnyMethod()
     .WithOrigins("http://localhost:4200", "https://localhost:4200")  // Specify both HTTP and HTTPS explicitly
     .AllowCredentials()  // Allow credentials like cookies or authentication tokens
);
app.UseAuthentication();

app.UseAuthorization();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<DataContext>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
    await context.Database.MigrateAsync();
    await Seed.SeedData(context, userManager, roleManager);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");

}
app.MapHub<NotificationHub>("/notificationHub");  // Define your SignalR hub route here

// Other routes (if you have any)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseHangfireDashboard();


RecurringJob.AddOrUpdate<IReceptDoctor>(
    "AddRandomReceptJob",
   service => service.AddRecept(),
     "*/5 * * * *" );

app.MapControllers();
app.Run();
