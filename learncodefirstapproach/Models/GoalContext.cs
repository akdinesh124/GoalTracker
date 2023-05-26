using Microsoft.EntityFrameworkCore;

namespace learncodefirstapproach.Models
{
    public class GoalContext :DbContext
    {
        public GoalContext(DbContextOptions<GoalContext> options) : base(options){ }

        public DbSet<GoalItem> GoalItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GoalItem>().HasData(
                new GoalItem() { Id = 1, Name = "Buy car", IsComplete =false, Description = "buy a good nice car" });
        } 
    }
}
