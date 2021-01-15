using System.Collections.Generic;
using System.Linq;
using LearningPlatform.Data;
using LearningPlatform.Models.AnswerModels;
using LearningPlatform.Models.AssignmentModels;
using LearningPlatform.Models.ConnectionModels;
using LearningPlatform.Models.CourseModels;
using LearningPlatform.Models.ModuleModels;
using LearningPlatform.Models.UserModels;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatform.Services
{
    public static class SerializationHelper
    {
        public static ApplicationDbContext CurrentDbContext;
        public static List<AnswerQuestionOption> AnswerQuestionOptions;
        public static List<AnswerThought> AnswerThoughts;
        public static List<AnswerAssignment> AnswerAssignments;
        public static List<Assignment> Assignments;
        public static List<CourseCategory> CourseCategories;
        public static List<CourseTeacher> CourseTeachers;
        public static List<Enrollment> Enrollments;
        public static List<Course> Courses;
        public static List<Lesson> Lessons;
        public static List<Module> Modules;
        public static List<QuestionOption> QuestionOptions;
        public static List<Question> Questions;
        public static List<Review> Reviews;
        public static List<Student> Students;
        public static List<Teacher> Teachers;
        public static List<Thought> Thoughts;

        public static void WipeDatabase()
        {
            CurrentDbContext.Database.ExecuteSqlRaw("delete from Students;");
            CurrentDbContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT ([Students], RESEED);");
            CurrentDbContext.Database.ExecuteSqlRaw("delete from Courses;");
            CurrentDbContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT ([Courses], RESEED);");
            CurrentDbContext.Database.ExecuteSqlRaw("delete from CourseCategories;");
            CurrentDbContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT ([CourseCategories], RESEED);");
            CurrentDbContext.Database.ExecuteSqlRaw("delete from Teachers;");
            CurrentDbContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT ([Teachers], RESEED);");
        }
        
        public static void SaveAllToDatabase()
        {
            
            AnswerQuestionOptions.Sort((i1, i2) => i1.Id - i2.Id); 
            AnswerThoughts.Sort((i1, i2) => i1.Id - i2.Id); 
            AnswerAssignments.Sort((i1, i2) => i1.Id - i2.Id); 
            Assignments.Sort((i1, i2) => i1.Id - i2.Id); 
            CourseCategories.Sort((i1, i2) => i1.Id - i2.Id); 
            CourseTeachers.Sort((i1, i2) => i1.Id - i2.Id); 
            Enrollments.Sort((i1, i2) => i1.Id - i2.Id); 
            Courses.Sort((i1, i2) => i1.Id - i2.Id); 
            Lessons.Sort((i1, i2) => i1.Id - i2.Id); 
            Modules.Sort((i1, i2) => i1.Id - i2.Id); 
            QuestionOptions.Sort((i1, i2) => i1.Id - i2.Id); 
            Questions.Sort((i1, i2) => i1.Id - i2.Id); 
            Reviews.Sort((i1, i2) => i1.Id - i2.Id); 
            Students.Sort((i1, i2) => i1.Id - i2.Id); 
            Teachers.Sort((i1, i2) => i1.Id - i2.Id); 
            Thoughts.Sort((i1, i2) => i1.Id - i2.Id);

            // var e14 = new Student {Name = "", Password = "", Surname = "", Username = ""};
            // CurrentDbContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT ([Students], RESEED, 0);");
            //  CurrentDbContext.Students.Add(e14);
            //  CurrentDbContext.SaveChanges();
            //
            // var e15 = new Teacher {Name = "", Username = "", Surname = "", Password = ""};
            //  CurrentDbContext.Teachers.Add(e15);
            //  CurrentDbContext.SaveChanges();
            //  
            // var e5 = new CourseCategory {Name = "", CoverImage = ""};
            //  CurrentDbContext.CourseCategories.Add(e5);
            //  CurrentDbContext.SaveChanges();
            //  
            // var e8 = new Course {Name = "", Description = "", Language = "", CoverImage = "", CourseCategoryId = CurrentDbContext.CourseCategories.First().Id};
            //  CurrentDbContext.Courses.Add(e8);
            //  CurrentDbContext.SaveChanges();
            //
            // var e10 = new Module { Description = "",Name = "", CourseId = CurrentDbContext.Courses.First().Id};
            //  CurrentDbContext.Modules.Add(e10);
            //  CurrentDbContext.SaveChanges();
            //
            // var e9 = new Lesson {Name = "", Content = "", ModuleId = CurrentDbContext.Modules.First().Id};
            //  CurrentDbContext.Lessons.Add(e9);
            //  CurrentDbContext.SaveChanges();
            //
            //
            // var e4 = new Assignment { Description = "",Name = "", ModuleId = CurrentDbContext.Modules.First().Id};
            //  CurrentDbContext.Assignments.Add(e4);
            //  CurrentDbContext.SaveChanges();
            //
            //
            // var e12 = new Question { Name = "", AssignmentId = CurrentDbContext.Assignments.First().Id};
            //  CurrentDbContext.Questions.Add(e12);
            //  CurrentDbContext.SaveChanges();
            //
            //
            // var e11 = new QuestionOption {Text = "", QuestionId = CurrentDbContext.Questions.First().Id};
            //  CurrentDbContext.QuestionOptions.Add(e11);
            //  CurrentDbContext.SaveChanges();
            //
            //
            // var e7 = new Enrollment {StudentId = CurrentDbContext.Students.First().Id, CourseId = CurrentDbContext.Courses.First().Id};
            //  CurrentDbContext.Enrollments.Add(e7);
            //  CurrentDbContext.SaveChanges();
            //
            //
            // var e13 = new Review { Text = "", CourseId = CurrentDbContext.Courses.First().Id, StudentId = CurrentDbContext.Students.First().Id};
            //  CurrentDbContext.Reviews.Add(e13);
            //  CurrentDbContext.SaveChanges();
            //
            //
            // var e16 = new Thought {Content = "", Name = "", AssignmentId = CurrentDbContext.Assignments.First().Id};
            //  CurrentDbContext.Thoughts.Add(e16);
            //  CurrentDbContext.SaveChanges();
            //                  
            //
            // var e6 = new CourseTeacher {CourseId = CurrentDbContext.Courses.First().Id, TeacherId = CurrentDbContext.Teachers.First().Id};
            //  CurrentDbContext.CourseTeachers.Add(e6);
            //  CurrentDbContext.SaveChanges();
            //
            // var e3 = new AnswerAssignment {AssignmentId = CurrentDbContext.Assignments.First().Id, StudentId = CurrentDbContext.Students.First().Id};
            //   CurrentDbContext.AnswerAssignments.Add(e3);
            //   CurrentDbContext.SaveChanges();
            //  
            // var e1 = new AnswerQuestionOption {AnswerAssignmentId = CurrentDbContext.AnswerAssignments.First().Id, QuestionOptionId = CurrentDbContext.QuestionOptions.First().Id};
            // CurrentDbContext.AnswerQuestionOptions.Add(e1);
            // CurrentDbContext.SaveChanges();
            //
            // var e2 = new AnswerThought { Content = "", AnswerAssignmentId = CurrentDbContext.AnswerAssignments.First().Id, ThoughtId = CurrentDbContext.Thoughts.First().Id};
            //  CurrentDbContext.AnswerThoughts.Add(e2);
            //  CurrentDbContext.SaveChanges();
            //
            //  WipeDatabase();

            bool first = true;
            foreach (var v in Students)
            {
                if (first)
                {
                    CurrentDbContext.Database.ExecuteSqlRaw(string.Format("DBCC CHECKIDENT ([Students], RESEED, {0})", v.Id) + ";");
                    first = false;
                }
                else
                  CurrentDbContext.Database.ExecuteSqlRaw(string.Format("DBCC CHECKIDENT ([Students], RESEED, {0})", v.Id-1) + ";");
                v.Id = 0;                              
                CurrentDbContext.Students.Add(v);
            CurrentDbContext.SaveChanges();
            }

            first = true;
            foreach (var v in CourseCategories)
            {
                if (first)
                {
                    CurrentDbContext.Database.ExecuteSqlRaw(string.Format("DBCC CHECKIDENT ([CourseCategories], RESEED, {0})", v.Id) + ";");
                    first = false;
                } else
                    CurrentDbContext.Database.ExecuteSqlRaw(string.Format("DBCC CHECKIDENT ([CourseCategories], RESEED, {0})", v.Id-1) + ";");
                CurrentDbContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT ([CourseCategories])");
                v.Id = 0;
                CurrentDbContext.CourseCategories.Add(v);
                CurrentDbContext.SaveChanges();
            }
            
            first = true;
            foreach (var v in Courses)
            {
                if (first)
                {
                 CurrentDbContext.Database.ExecuteSqlRaw(string.Format("DBCC CHECKIDENT ([Courses], RESEED, {0})", v.Id) + ";");
                    first = false;
                } else 
                 CurrentDbContext.Database.ExecuteSqlRaw(string.Format("DBCC CHECKIDENT ([Courses], RESEED, {0})", v.Id-1) + ";");
                v.Id = 0;               
                CurrentDbContext.Courses.Add(v);
            CurrentDbContext.SaveChanges();
            }

            first = true; 
            foreach (var v in Modules)
            {
                if (first)
                {
                  CurrentDbContext.Database.ExecuteSqlRaw(string.Format("DBCC CHECKIDENT ([Modules], RESEED, {0})", v.Id) + ";");
                    first = false;
                } else 
                  CurrentDbContext.Database.ExecuteSqlRaw(string.Format("DBCC CHECKIDENT ([Modules], RESEED, {0})", v.Id-1) + ";");
                v.Id = 0;                              
                CurrentDbContext.Modules.Add(v);
            CurrentDbContext.SaveChanges();
            }

            first = true; 
            foreach (var v in Lessons)
            {
                if (first)
                {
                  CurrentDbContext.Database.ExecuteSqlRaw(string.Format("DBCC CHECKIDENT ([Lessons], RESEED, {0})", v.Id) + ";");
                  first = false;
                } else
                  CurrentDbContext.Database.ExecuteSqlRaw(string.Format("DBCC CHECKIDENT ([Lessons], RESEED, {0})", v.Id-1) + ";");
                v.Id = 0;                              
                CurrentDbContext.Lessons.Add(v);
            CurrentDbContext.SaveChanges();
            }

            first = true; 
            foreach (var v in Assignments)
            {
                if (first)
                {
                  CurrentDbContext.Database.ExecuteSqlRaw(string.Format("DBCC CHECKIDENT ([Assignments], RESEED, {0})", v.Id) + ";");
                    first = false;
                } else 
                  CurrentDbContext.Database.ExecuteSqlRaw(string.Format("DBCC CHECKIDENT ([Assignments], RESEED, {0})", v.Id-1) + ";");
                v.Id = 0;                              
                CurrentDbContext.Assignments.Add(v);
            CurrentDbContext.SaveChanges();
            }

            first = true; 
            foreach (var v in Questions)
            {
                if (first)
                {
                    
                  CurrentDbContext.Database.ExecuteSqlRaw(string.Format("DBCC CHECKIDENT ([Questions], RESEED, {0})", v.Id) + ";");
                  first = false;
                } else 
                  CurrentDbContext.Database.ExecuteSqlRaw(string.Format("DBCC CHECKIDENT ([Questions], RESEED, {0})", v.Id-1) + ";");
                v.Id = 0;                              
                CurrentDbContext.Questions.Add(v);
            CurrentDbContext.SaveChanges();
            }


            first = true; 
            foreach (var v in QuestionOptions)
            {
                if (first)
                {
                  CurrentDbContext.Database.ExecuteSqlRaw(string.Format("DBCC CHECKIDENT ([QuestionOptions], RESEED, {0})", v.Id) + ";");
                  first = false;
                } else 
                  CurrentDbContext.Database.ExecuteSqlRaw(string.Format("DBCC CHECKIDENT ([QuestionOptions], RESEED, {0})", v.Id-1) + ";");
                v.Id = 0;                              
                CurrentDbContext.QuestionOptions.Add(v);
            CurrentDbContext.SaveChanges();
            }
            
            foreach (var v in Enrollments)
            {
                  // CurrentDbContext.Database.ExecuteSqlRaw(string.Format("DBCC CHECKIDENT ([Enrollments], RESEED, {0})", v.Id-1) + ";");
                  // v.Id = 0;                              
                CurrentDbContext.Enrollments.Add(v);
            
            CurrentDbContext.SaveChanges();
            }

            first = true;
            foreach (var v in Reviews)
            {
                if (first)
                {
                  CurrentDbContext.Database.ExecuteSqlRaw(string.Format("DBCC CHECKIDENT ([Reviews], RESEED, {0})", v.Id) + ";");
                    first = false;
                } else 
                  CurrentDbContext.Database.ExecuteSqlRaw(string.Format("DBCC CHECKIDENT ([Reviews], RESEED, {0})", v.Id-1) + ";");
                v.Id = 0;                              
                CurrentDbContext.Reviews.Add(v);
            CurrentDbContext.SaveChanges();
            }

            first = true; 
            foreach (var v in Teachers)
            {
                if (first)
                {
                    
                  CurrentDbContext.Database.ExecuteSqlRaw(string.Format("DBCC CHECKIDENT ([Teachers], RESEED, {0})", v.Id) + ";");
                    first = false;
                } else
                  CurrentDbContext.Database.ExecuteSqlRaw(string.Format("DBCC CHECKIDENT ([Teachers], RESEED, {0})", v.Id-1) + ";");
                v.Id = 0;                              
                CurrentDbContext.Teachers.Add(v);
            CurrentDbContext.SaveChanges();
            }


            first = true;
            foreach (var v in Thoughts)
            {
                if (first)
                {
                    CurrentDbContext.Database.ExecuteSqlRaw(string.Format("DBCC CHECKIDENT ([Thoughts], RESEED, {0})", v.Id) + ";");
                    first = false;
                } else 
                    CurrentDbContext.Database.ExecuteSqlRaw(string.Format("DBCC CHECKIDENT ([Thoughts], RESEED, {0})", v.Id-1) + ";");
                v.Id = 0;                              
                CurrentDbContext.Thoughts.Add(v);
            CurrentDbContext.SaveChanges();
            }

            first = true;
            foreach (var v in CourseTeachers)
            {
                if (first)
                {
                  CurrentDbContext.Database.ExecuteSqlRaw(string.Format("DBCC CHECKIDENT ([CourseTeachers], RESEED, {0})", v.Id) + ";");
                    first = false;
                } else 
                  CurrentDbContext.Database.ExecuteSqlRaw(string.Format("DBCC CHECKIDENT ([CourseTeachers], RESEED, {0})", v.Id-1) + ";");
                v.Id = 0;                              
                CurrentDbContext.CourseTeachers.Add(v);
            CurrentDbContext.SaveChanges();
            }

            first = true; 
            foreach (var v in AnswerAssignments)
            {
                if (first)
                {
                  CurrentDbContext.Database.ExecuteSqlRaw(string.Format("DBCC CHECKIDENT ([AnswerAssignments], RESEED, {0})", v.Id) + ";");
                    first = false;
                } else 
                  CurrentDbContext.Database.ExecuteSqlRaw(string.Format("DBCC CHECKIDENT ([AnswerAssignments], RESEED, {0})", v.Id-1) + ";");
                v.Id = 0;                              
                CurrentDbContext.AnswerAssignments.Add(v);
            CurrentDbContext.SaveChanges();
            }

            first = true; 
            foreach (var v in AnswerThoughts)
            {
                if (first)
                {
                  CurrentDbContext.Database.ExecuteSqlRaw(string.Format("DBCC CHECKIDENT ([AnswerThoughts], RESEED, {0})", v.Id) + ";");
                    first = false;
                } else 
                    CurrentDbContext.Database.ExecuteSqlRaw(string.Format("DBCC CHECKIDENT ([AnswerThoughts], RESEED, {0})", v.Id-1) + ";");
                v.Id = 0;                              
                CurrentDbContext.AnswerThoughts.Add(v);
            CurrentDbContext.SaveChanges();
            }


            first = true;
            foreach (var v in AnswerQuestionOptions)
            {
                if (first)
                {
                  CurrentDbContext.Database.ExecuteSqlRaw(string.Format("DBCC CHECKIDENT ([AnswerQuestionOptions], RESEED, {0})", v.Id) + ";");
                    first = false;
                } else 
                  CurrentDbContext.Database.ExecuteSqlRaw(string.Format("DBCC CHECKIDENT ([AnswerQuestionOptions], RESEED, {0})", v.Id-1) + ";");
                v.Id = 0;                              
                CurrentDbContext.AnswerQuestionOptions.Add(v);
            CurrentDbContext.SaveChanges();
            }
            
        }
    }
}