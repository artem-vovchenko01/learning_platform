using System.Linq;
using LearningPlatform.Data;
using LearningPlatform.Models.ModuleModels;
using LearningPlatform.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatform.Services
{
    public static class CourseService
    {
        public static StudyCourseViewModel ComposeCourseModel(ApplicationDbContext db, int courseId)
        {
            var model = new StudyCourseViewModel
            {
                Modules = db.Modules
                    .Where(m => m.CourseId == courseId)
                    .Include(m => m.Lessons)
                    .Include(m => m.Assignments),
                
                Course = db.Courses.FirstOrDefault(c => c.Id == courseId)
            };
            return model;
        }

        public static StudyCourseViewModel ComposeCourseModel(ApplicationDbContext db, int courseId, int moduleId)
        {
            var model = ComposeCourseModel(db, courseId);
            model.Module = db.Modules.Find(moduleId);
            var filledModule = model.Modules.FirstOrDefault(m => m.Id == moduleId);
            model.Module.Lessons = filledModule?.Lessons;
            model.Module.Assignments = filledModule?.Assignments;
            return model;
        }

        public static void FillModuleData(ApplicationDbContext db, StudyCourseViewModel model, int moduleId)
        {
              model.Module = db.Modules.Find(moduleId);
              model.Module.Lessons = db.Lessons.Where(l => l.ModuleId == moduleId).ToList();
              model.Module.Assignments = db.Assignments.Where(a => a.ModuleId == moduleId).ToList();
        }

        public static StudyCourseViewModel ComposeCourseModel(ApplicationDbContext db, int courseId, int moduleId,
            int assignmentId)
        {
            var model = ComposeCourseModel(db, courseId, moduleId);
            model.Assignment = db.Assignments.Find(assignmentId);
            model.Assignment.Questions = db.Questions.Where(q => q.AssignmentId == assignmentId)
                .Include(q => q.QuestionOptions)
                .ToList();
            model.Assignment.Thoughts = db.Thoughts.Where(t => t.AssignmentId == assignmentId).ToList();
            return model;
        }

        public static ViewCourseViewModel ComposeViewCourseViewModel(ApplicationDbContext db, int courseId)
        {
                var course = db.Courses.Find(courseId);
                var model = new ViewCourseViewModel
                {
                    Course = course,
                    Enrollments = db.Enrollments.Where(e => e.CourseId == courseId),
                    Reviews = db.Reviews.Where(r => r.CourseId == courseId).Include(r => r.Student),
                    AverageReview = AverageReview(db, courseId),
                    Modules = db.Modules.Where(m => m.CourseId == courseId)
                };
                if (StudentService.LoggedInStudent != null) model.IsEnrolled = EnrollmentService.IsEnrolled(db, courseId);
                 return model;
        }
        
         public static void ResolveOrdersAfterDelete(ApplicationDbContext db, StudyCourseViewModel model,
             int orderDeleted)
         {
             foreach (var lesson in model.Module.Lessons.Where(l => l.Order > orderDeleted))
             {
                 lesson.Order--;
                 db.Lessons.Update(lesson);
             }
 
             foreach (var assignment in model.Module.Assignments.Where(a => a.Order > orderDeleted))
             {
                 assignment.Order--;
                 db.Assignments.Update(assignment);
             }
 
             db.SaveChanges();
         }

         public static double AverageReview(ApplicationDbContext db, int courseId)
         {
             long count = 0;
             long found = 0;
             foreach (var review in db.Reviews.Where(r => r.CourseId == courseId))
             {
                 count += review.Rank;
                 found++;
             }
             return found == 0 ? 0 : double.Parse($"{(double) count / found:0.00}");
         }
    }
}