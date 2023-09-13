using Microsoft.EntityFrameworkCore;
using capstone.Models;

namespace capstone.DataContext
{
    public class ShoppingmallDbContext : DbContext
    {
        public ShoppingmallDbContext(DbContextOptions<ShoppingmallDbContext> options) : base(options) 
        { 

        }

        public DbSet<Member> Member { get; set; }
        public DbSet<MemberLogin> MemberLogin { get; set; }
        
    }
}
