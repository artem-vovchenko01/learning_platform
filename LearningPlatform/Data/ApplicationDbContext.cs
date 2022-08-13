using LearningPlatform.Models.AnswerModels;
using LearningPlatform.Models.AssignmentModels;
using LearningPlatform.Models.ConnectionModels;
using LearningPlatform.Models.CourseModels;
using LearningPlatform.Models.ModuleModels;
using LearningPlatform.Models.UserModels;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatform.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        
        public DbSet<AnswerQuestionOption> AnswerQuestionOptions { get; set; }
        public DbSet<AnswerThought> AnswerThoughts { get; set; }
        public DbSet<AnswerAssignment> AnswerAssignments { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<CourseCategory> CourseCategories { get; set; }
        public DbSet<CourseTeacher> CourseTeachers { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<QuestionOption> QuestionOptions { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Student> Students { get; set; }
        
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Thought> Thoughts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enrollment>()
                .HasKey(e => new {e.CourseId, e.StudentId});
        }
    }
}
