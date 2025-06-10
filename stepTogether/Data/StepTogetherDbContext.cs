using Microsoft.EntityFrameworkCore;
using stepTogether.Controllers;
using stepTogether.Models;
using static System.Net.Mime.MediaTypeNames;


namespace stepTogether.Data
{
    public class StepTogetherDbContext : DbContext
    {
        public StepTogetherDbContext(DbContextOptions<StepTogetherDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Posts> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);  // 呼叫基底類別的方法
            // 強制資料表名稱為小寫
            modelBuilder.Entity<Posts>().ToTable("posts");
            modelBuilder.Entity<Test>().ToTable("test");
        }


    }
}
