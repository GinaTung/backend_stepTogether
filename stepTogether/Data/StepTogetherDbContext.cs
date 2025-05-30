using Microsoft.EntityFrameworkCore;
using stepTogether.Controllers;
using stepTogether.Models;


namespace stepTogether.Data
{
    public class StepTogetherDbContext : DbContext
    {
        public StepTogetherDbContext(DbContextOptions<StepTogetherDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Posts> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);  // 呼叫基底類別的方法

            // 明確將 Company 類別映射到小寫的 "companies" 資料表
            //modelBuilder.Entity<Company>().ToTable("companies");

            // 強制資料表名稱為小寫
            modelBuilder.Entity<Posts>().ToTable("posts");
        }


    }
}
