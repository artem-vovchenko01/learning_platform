using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LearningPlatform.Data;
using LearningPlatform.Models.CourseModels;
using LearningPlatform.Models.UserModels;
using LearningPlatform.Services;
using LearningPlatform.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatform.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;  
        
        public CourseController(ApplicationDbContext db, IWebHostEnvironment hostEnvironment )
        {
            _db = db;
            _webHostEnvironment = hostEnvironment;
        }
       
        // GET
        public IActionResult Index()
        {
            IEnumerable<Course> courses = _db.Courses;
            return View(courses);
        }

        public IActionResult ViewByCategory(int categoryId)
        {
            IEnumerable<Course> courses = _db.Courses.Where(c => c.CourseCategoryId == categoryId);
            ViewBag.Category = _db.CourseCategories.Find(categoryId);
            return View("~/Views/Course/Index.cshtml", courses);
        }
        
        // GET - CREATE
         public IActionResult Create()
         {
             var model = new CreateCourseViewModel
             {
                 Course = new Course(), 
                 CourseCategories = new SelectList(_db.CourseCategories, "Id", "Name")
             };
             return View(model);
         }       
        
         // POST - CREATE
         [HttpPost]
         [ValidateAntiForgeryToken]
          public IActionResult Create(CreateCourseViewModel model)
          {
              var fileName = UploadedFile(model);
              model.Course.CoverImage = fileName;
              _db.Courses.Add(model.Course);
              _db.SaveChanges();
              return RedirectToAction("Index");
          }
          
          private string UploadedFile(CreateCourseViewModel model)  
          {  
              string uniqueFileName = null;  
  
              if (model.CoverPicture != null)  
              {  
                  var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");  
                  uniqueFileName = Guid.NewGuid().ToString() + "_" + model.CoverPicture.FileName;  
                  var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                  using var fileStream = new FileStream(filePath, FileMode.Create);
                  model.CoverPicture.CopyTo(fileStream);
              }  
              return uniqueFileName;  
          }    
          
         // GET - EDIT
          public IActionResult Edit(int courseId)
          {
              var course = _db.Courses.Find(courseId);
              if (course == null) return NotFound();
                            
              var model = new CreateCourseViewModel
             {
                 Course = course,
                 CourseCategories = new SelectList(_db.CourseCategories, "Id", "Name")
             };

              return View(model);
          }                      
          
          // POST - EDIT
          [HttpPost]
          [ValidateAntiForgeryToken]
           public IActionResult Edit(CreateCourseViewModel model)
           {
               var fileName = UploadedFile(model);
               if (fileName != null) model.Course.CoverImage = fileName;
               else model.Course.CoverImage = Request.Form["oldPicture"];
                   _db.Courses.Update(model.Course);
                   _db.SaveChanges();
                   return RedirectToAction("Index");
           }     
           
          // GET - DELETE 
           public IActionResult Delete(int courseId)
           {
               var course = _db.Courses.Find(courseId);
               if (course == null) return NotFound();
               
               return View(course);
           }                      
           
           // POST - DELETE
           [HttpPost]
           [ValidateAntiForgeryToken]
            public IActionResult DeletePost(Course course)
            {
                var foundCourse = _db.Courses.Find(course.Id);
                _db.Courses.Remove(foundCourse);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }          
            
            
            // GET - VIEW_COURSE
            public IActionResult ViewCourse(int courseId)
            {
                var model = CourseService.ComposeViewCourseViewModel(_db, courseId);
                return View(model);
            }
            
            // GET - ENROLL
            public IActionResult Enroll(int courseId)
            {
                 var obj = _db.Courses.Find(courseId);
                 if (obj == null)
                 {
                     return NotFound();
                 }               
                
                 EnrollmentService.Enroll(_db, courseId);
                 return RedirectToAction("ViewCourse", new {courseId});
            }
            
           // POST - RATING 
           public IActionResult Review(int courseId)
           {
               var review = new Review
               {
                   StudentId = StudentService.LoggedInStudent.Id,
                   Text = Request.Form["ReviewText"],
                   Rank = int.Parse(Request.Form["SelectedRating"]),
                   DateTime = DateTime.Now,
                   CourseId = courseId
               };
               _db.Reviews.Add(review);
               _db.SaveChanges();
                 return RedirectToAction("ViewCourse", new {courseId});
           }

           // GET - DELETE REVIEW
           public IActionResult DeleteReview(int reviewId, int courseId)
           {
               var r = _db.Reviews.FirstOrDefault(rev => rev.Id == reviewId);
               _db.Reviews.Remove(r);
               _db.SaveChanges();
               return RedirectToAction("ViewCourse", new {courseId});
           }

           // GET - UNENROLL
           public IActionResult Unenroll(int courseId)
           {
               EnrollmentService.Unenroll(_db, courseId);
               return RedirectToAction("ViewCourse", new {courseId});
           }

           // GET - STUDY
           public IActionResult Study(int courseId)
           {
               var model = CourseService.ComposeCourseModel(_db, courseId);
               return View("Study", model);
           }

           private void InsertionSort(List<Student> inputArray, ViewDataDictionary viewData)
           {
               for (int i = 0; i < inputArray.Count- 1; i++)
               {
                   for (int j = i + 1; j > 0; j--)
                   {
                       int s1 = (int) viewData["student" + inputArray.ElementAt(j- 1).Id + "Enrollments"];
                       int s2 = (int) viewData["student" + inputArray.ElementAt(j).Id + "Enrollments"];
                       if (s2 > s1)
                       {
                           Student temp = inputArray.ElementAt(j -1);
                           inputArray[j - 1] = inputArray[j];
                           inputArray[j] = temp;
                       }
                   }
               }      
           }
           
           public IActionResult Search(string advanced, string showStudents)
           {
            var courses = _db.Courses.Include(c => c.Enrollments).ToList();
                    ViewBag.MaxEntries = courses.Count;
                    foreach (var c in courses)
                    {
                        c.AverageReview = Services.CourseService.AverageReview(_db, c.Id);
                    }              
                    var sortOption = "";
                    var filterOption = "";
                    try
                    {
                        sortOption = Request.Form["sortOption"];
                        filterOption = Request.Form["filterOption"];
                     switch (sortOption)
                     {
                         case "Name":
                             if (Request.Form["order"] == "Descending")
                             {
                                 courses = courses.OrderByDescending(c => c.Name).ToList();
                             }
                             else
                             {
                                 courses = courses.OrderBy(c => c.Name).ToList();
                             }
                             break;
                         case "Description":
                             if (Request.Form["order"] == "Descending")
                             {
                                 courses = courses.OrderByDescending(c => c.Description).ToList();
                             }
                             else
                             {
                                 courses = courses.OrderBy(c => c.Description).ToList();
                             }
                             break;
                         case "Enrollments":
                             if (Request.Form["order"] == "Descending")
                             {
                                 courses = courses.OrderByDescending(c => c.Enrollments.Count).ToList();
 
                             }
                             else
                             {
                                 courses = courses.OrderBy(c => c.Enrollments.Count).ToList();
                             }
                             break;
                         case "Rating":
                             if (Request.Form["order"] == "Descending")
                             {
                                 courses = courses.OrderByDescending(c => c.AverageReview).ToList();
                             }
                             else
                             {
                                 courses = courses.OrderBy((c => c.AverageReview)).ToList();
                             }
                             break;
                     }
                     if (Request.Form["numberEntries"] != "")
                     {
                         var numberEntries = int.Parse(Request.Form["numberEntries"]);
                         courses = courses.GetRange(0, numberEntries);
                     }
                       
                    }
                    catch (Exception e)
                    {
                        // ignored
                    }

                    if (showStudents == "true")
                    {
                       var students = new List<Student>();
                       foreach (var c in courses)
                       {
                           foreach (var e in c.Enrollments)
                           {
                               var student = _db.Students.FirstOrDefault(s => s.Id == e.StudentId);
                               if (!students.Contains(student))
                               {
                                   students.Add(student);
                               }

                               var str = "student" + student.Id + "Enrollments";
                                   if (ViewData[str] == null) ViewData[str] = 1;
                                   else ViewData[str] = ((int) ViewData[str]) + 1;
                           }

                            InsertionSort(students, ViewData);
                           ViewBag.Students = students;
                       }

                    }

                    ViewBag.ShowStudents = showStudents == "true";
                    ViewBag.Advanced = advanced == "true";
                    return View(courses);              
           }
           
    }
}