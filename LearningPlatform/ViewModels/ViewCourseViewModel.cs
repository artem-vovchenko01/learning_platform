using System.Collections;
using System.Collections.Generic;
using LearningPlatform.Models;
using LearningPlatform.Models.ConnectionModels;
using LearningPlatform.Models.CourseModels;
using LearningPlatform.Models.ModuleModels;

namespace LearningPlatform.ViewModels
{
    public class ViewCourseViewModel
    {
        public Course Course { get; set; }
        public IEnumerable<Enrollment> Enrollments { get; set; }
        public bool IsEnrolled { get; set; }
        public double AverageReview { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
        public IEnumerable<Module> Modules { get; set; }
    }
}