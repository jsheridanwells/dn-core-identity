using Microsoft.EntityFrameworkCore;

namespace AuthService.Entities
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> opts) : base(opts) { }
        
        public DbSet<User> Users { get; set; }    
         
    }
}