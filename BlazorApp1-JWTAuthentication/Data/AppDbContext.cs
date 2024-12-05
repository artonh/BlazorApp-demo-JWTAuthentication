using BlazorApp1.JWTAuthentication.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.JWTAuthentication.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }

        public DbSet<ApplicationUser> Users { get; set; }
    }
}
