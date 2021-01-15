using System.Collections.Generic;
using System.Linq;
using LearningPlatform.Models.AnswerModels;
using LearningPlatform.Models.AssignmentModels;
using LearningPlatform.Models.ConnectionModels;
using LearningPlatform.Models.CourseModels;
using LearningPlatform.Models.ModuleModels;
using LearningPlatform.Models.UserModels;
using LearningPlatform.Services;

namespace LearningPlatform.Models
{
    public class EmulatedDb
    {
        private EmulatedDb() {}
        private static EmulatedDb _instance;

        public static EmulatedDb Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EmulatedDb();
                }
                return _instance;
            }
            set => _instance = value;
        }

        public List<AnswerQuestionOption> AnswerQuestionOptions
        {
            get { return SerializationHelper.CurrentDbContext.AnswerQuestionOptions.ToList(); }
            set => SerializationHelper.AnswerQuestionOptions = value;
        }
        
 
        public List<AnswerThought> AnswerThoughts
        {
            get { return SerializationHelper.CurrentDbContext.AnswerThoughts.ToList(); }
            set => SerializationHelper.AnswerThoughts = value;
        }
        
 
        public List<AnswerAssignment> AnswerAssignments
        {
            get { return SerializationHelper.CurrentDbContext.AnswerAssignments.ToList(); }
            set => SerializationHelper.AnswerAssignments = value;
        }
        
 
        public List<Assignment> Assignments
        {
            get { return SerializationHelper.CurrentDbContext.Assignments.ToList(); }
            set => SerializationHelper.Assignments = value;
        }
        
 
        public List<CourseCategory> CourseCategories
        {
            get { return SerializationHelper.CurrentDbContext.CourseCategories.ToList(); }
            set => SerializationHelper.CourseCategories = value;
        }
        
 
        public List<CourseTeacher> CourseTeachers
        {
            get { return SerializationHelper.CurrentDbContext.CourseTeachers.ToList(); }
            set => SerializationHelper.CourseTeachers = value;
        }
        
 
        public List<Enrollment> Enrollments
        {
            get { return SerializationHelper.CurrentDbContext.Enrollments.ToList(); }
            set => SerializationHelper.Enrollments = value;
        }
        
 
        public List<Course> Courses
        {
            get { return SerializationHelper.CurrentDbContext.Courses.ToList(); }
            set => SerializationHelper.Courses = value;
        }
        
 
        public List<Lesson> Lessons
        {
            get { return SerializationHelper.CurrentDbContext.Lessons.ToList(); }
            set => SerializationHelper.Lessons = value;
        }
        
 
        public List<Module> Modules
        {
            get { return SerializationHelper.CurrentDbContext.Modules.ToList(); }
            set => SerializationHelper.Modules = value;
        }
        
 
        public List<QuestionOption> QuestionOptions
        {
            get { return SerializationHelper.CurrentDbContext.QuestionOptions.ToList(); }
            set => SerializationHelper.QuestionOptions = value;
        }
        
 
        public List<Question> Questions
        {
            get { return SerializationHelper.CurrentDbContext.Questions.ToList(); }
            set => SerializationHelper.Questions = value;
        }
        
 
        public List<Review> Reviews
        {
            get { return SerializationHelper.CurrentDbContext.Reviews.ToList(); }
            set => SerializationHelper.Reviews = value;
        }
        
 
        public List<Student> Students
        {
            get { return SerializationHelper.CurrentDbContext.Students.ToList(); }
            set => SerializationHelper.Students = value;
        }
        
         public List<Teacher> Teachers    
         {
             get { return SerializationHelper.CurrentDbContext.Teachers.ToList(); }
             set => SerializationHelper.Teachers = value;
         }
         
           public List<Thought> Thoughts
           {
               get { return SerializationHelper.CurrentDbContext.Thoughts.ToList(); }
               set => SerializationHelper.Thoughts = value;
           }
           
                        
        
    }
}