using LoginDashboardApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LoginDashboardApi.Data
{
    public class userDbContext : DbContext
    {
       public userDbContext(DbContextOptions<userDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
    }
}
