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
        public DbSet<ProductInfo> ProductInfo { get; set; }
        public DbSet<Product_Mainboard> Product_Mainboard { get; set;}

        //자동생성되는 Discriminator생성 방지를 위한 메서드
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<ProductInfo>().HasNoDiscriminator();
        //}

        //        modelBuilder.Entity<ProductInfo>()
        //.HasDiscriminator<string>("MyDiscriminatorColumn") // `Discriminator` 열 생성 비활성화
        //.HasValue<Product_Mainboard>("Child");



        //base.OnModelCreating(modelBuilder);

        //modelBuilder.Entity<ProductInfo>()
        //    .ToTable("ProductsInfo");

        //modelBuilder.Entity<Product_Mainboard>().HasBaseType<ProductInfo>();


    }
}
