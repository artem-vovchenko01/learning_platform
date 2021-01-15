using System.Collections.Generic;
using LearningPlatform.Models;
using LearningPlatform.Models.AnswerModels;
using LearningPlatform.Models.AssignmentModels;
using LearningPlatform.Models.CourseModels;
using LearningPlatform.Models.ModuleModels;

namespace LearningPlatform.ViewModels
{
    public class StudyCourseViewModel
    {
        public IEnumerable<Module> Modules { get; set; }
        public Course Course { get; set; }
        public Module CreatedModule { get; set; }
        public Lesson Lesson { get; set; }
        public Module Module { get; set; }
        public Assignment Assignment { get; set; }
        public AnswerAssignment AnswerAssignment { get; set; }
    }
}