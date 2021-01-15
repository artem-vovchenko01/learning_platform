using System.Linq;
using LearningPlatform.Data;
using LearningPlatform.Models;
using LearningPlatform.Models.ConnectionModels;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatform.Services
{
    public class EnrollmentService
    {
        public static void Enroll(ApplicationDbContext db, int courseId)
        {
           Enrollment enrollment = new Enrollment();
           // enrollment.Course = db.Courses.Find(courseId);
           // enrollment.Student = Program.LoggedInStudent;
           enrollment.CourseId = courseId;
           enrollment.StudentId = StudentService.LoggedInStudent.Id;
           db.Enrollments.Add(enrollment);
           db.SaveChanges();
        }

        public static bool IsEnrolled(ApplicationDbContext db, int? courseId)
        {
            int studentId = StudentService.LoggedInStudent.Id;
            return db.Enrollments.Any(e => e.CourseId == courseId
                                           && e.StudentId == studentId);
        }

        public static void Unenroll(ApplicationDbContext db, int courseId)
        {
            int studentId = StudentService.LoggedInStudent.Id;
            Enrollment enrollment = db.Enrollments
                .FirstOrDefault(e => e.CourseId == courseId && e.StudentId == studentId);
            db.Enrollments.Remove(enrollment);
            db.SaveChanges();    
        }
    }
}