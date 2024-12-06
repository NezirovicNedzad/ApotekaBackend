using ApotekaBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace ApotekaBackend.Data
{
    public class DataContext(DbContextOptions options) : DbContext(options)
    {

        public DbSet<AppUser> Users { get; set; }

    }
}
