
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace SportsStore.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {
            Database.EnsureCreated(); // nếu database chưa có nó tự sinh ra database cho mình luôn
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }  // tạo cơ sở dữ liệu cho Order 
    }
}
