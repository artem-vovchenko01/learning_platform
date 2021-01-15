using System.Collections.Generic;
using System.Linq;
using LearningPlatform.Data;
using LearningPlatform.Models;
using LearningPlatform.Models.CourseModels;
using LearningPlatform.Services;
using LearningPlatform.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LearningPlatform.Controllers
{
    public class ModuleController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ModuleController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Course> courses = _db.Courses;
            return null;
        }

        public IActionResult CreateModule(int courseId)
        {
            var model = CourseService.ComposeCourseModel(_db, courseId);
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateModule(StudyCourseViewModel model, int courseId)
        {
            model.CreatedModule.CourseId = courseId;
            _db.Add(model.CreatedModule);
            _db.SaveChanges();
            return RedirectToAction("Study", "Course", new {courseId});
        }
    }

}