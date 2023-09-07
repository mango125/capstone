using Microsoft.EntityFrameworkCore;
using capstone.Models;

namespace capstone.DataContext
{
    public class ShoppingmallDbContext : DbContext
    {
        public ShoppingmallDbContext(DbContextOptions<ShoppingmallDbContext> options) : base(options) 
        { 
        }

        public DbSet<Member> Members { get; set; }

        
    }
}
