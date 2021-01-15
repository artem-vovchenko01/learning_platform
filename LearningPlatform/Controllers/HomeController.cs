using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using LearningPlatform.Data;
using Microsoft.AspNetCore.Mvc;
using LearningPlatform.Models;
using LearningPlatform.Models.CourseModels;
using LearningPlatform.Models.UserModels;
using LearningPlatform.Services;
using LearningPlatform.ViewModels;
using Microsoft.AspNetCore.Hosting;

namespace LearningPlatform.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        
        private readonly IWebHostEnvironment _webHostEnvironment;  
        public HomeController(ApplicationDbContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _webHostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            SerializationHelper.CurrentDbContext = _db;
            ViewBag.NumberOfCourses = _db.Courses.Count();
            ViewBag.NumberOfStudents = _db.Students.Count();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            var model = new CreateStudentViewModel()
            {
                Student = new Student()
            };
            return View(model);
        }
        
        [HttpPost]
        public IActionResult Login(Student loginStudent)
        {
            var student = _db.Students.FirstOrDefault(s => s.Username == loginStudent.Username);
            if (student == null) return RedirectToAction("Login");
            if (student.Password == loginStudent.Password) StudentService.LoggedInStudent = student;
            else return RedirectToAction("Login");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Register(CreateStudentViewModel model)
        {
            var newStudent = model.Student;
            newStudent.RegistrationDate = DateTime.Now;
            var fileName = UploadedFile(model);
            newStudent.ProfileImage = fileName;
           _db.Add(newStudent);
           _db.SaveChanges();
           StudentService.LoggedInStudent = newStudent;
            return RedirectToAction("Index");
        }
          private string UploadedFile(CreateStudentViewModel model)  
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
        public IActionResult LogOut()
        {
            StudentService.LoggedInStudent = null;
            return RedirectToAction("Index");
        }
        
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        public IActionResult Edit(int studentId)
        {
            var student = studentId != 0 ? _db.Students.Find(studentId) : StudentService.LoggedInStudent;
            var model = new CreateStudentViewModel()
            {
                Student = student
            };
            return View(model);
        }
        
        [HttpPost]
         public IActionResult Edit(CreateStudentViewModel model)
         {
               var fileName = UploadedFile(model);
               if (fileName != null) model.Student.ProfileImage = fileName;
               else model.Student.ProfileImage = Request.Form["oldPicture"];
             _db.Students.Update(model.Student);
             _db.SaveChanges();
             StudentService.LoggedInStudent = _db.Students.Find(model.Student.Id);
             return RedirectToAction("Index");
         }

         public IActionResult DeleteAccount(int studentId)
         {
             var student = _db.Students.Find(studentId);
             _db.Students.Remove(student);
             _db.SaveChanges();
             StudentService.LoggedInStudent = null;
             return RedirectToAction("Index");
         }

         public IActionResult ViewEnrollments(int studentId)
         {
             var courses = new List<Course>();
             var enrollments = _db.Enrollments.Where(e => e.StudentId == studentId);
             foreach (var e in enrollments)
             {
                 courses.Add(_db.Courses.FirstOrDefault(c => c.Id == e.CourseId));
             }

             if (StudentService.LoggedInStudent != null && StudentService.LoggedInStudent.Id != studentId)
             {
                 ViewData["student"] = _db.Students.FirstOrDefault(s => s.Id == studentId);
             }
             return View(courses);
         }
    }
}
