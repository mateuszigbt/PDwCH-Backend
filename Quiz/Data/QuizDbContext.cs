using Microsoft.EntityFrameworkCore;
using Quiz.Model;

namespace Quiz.Data
{
    public class QuizDbContext : DbContext
    {
        public QuizDbContext(DbContextOptions<QuizDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>();
            modelBuilder.Entity<Quiz.Model.Quiz>()
                .Property(q => q.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Category>();
            modelBuilder.Entity<Answer>();
            modelBuilder.Entity<CorrectAnswer>();
            modelBuilder.Entity<Image>();
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Quiz.Model.Quiz> Quiz { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<CorrectAnswer> CorrectAnswers { get; set; }
        //public DbSet<Image> Images { get; set; }
    }
}
