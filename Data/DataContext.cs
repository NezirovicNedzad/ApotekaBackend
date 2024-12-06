using ApotekaBackend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApotekaBackend.Data
{
    public class DataContext(DbContextOptions options) :IdentityDbContext<AppUser,AppRole,int,
        IdentityUserClaim<int>,AppUserRole,
        IdentityUserLogin<int>,IdentityRoleClaim<int>,
        IdentityUserToken<int>>(options)
    {



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>().HasMany(appUser => appUser.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId)
                .IsRequired();

            builder.Entity<AppRole>().HasMany(appUser => appUser.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId)
                .IsRequired();
        }


    }
}
