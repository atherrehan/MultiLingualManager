using Microsoft.EntityFrameworkCore;
using MLSM.Api.Models;
namespace MLSM.Api.DBContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<LanguageStringEntity> MultilingualStrings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LanguageStringEntity>(entity =>
            {
                entity.ToTable("UILanguageLabels"); 
                entity.HasKey(e => e.Code);
            });
        }
    }
}
