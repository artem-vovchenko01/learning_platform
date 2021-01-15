using System.Linq;
using LearningPlatform.Data;
using LearningPlatform.Services;
using LearningPlatform.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LearningPlatform.Controllers
{
    public class LessonController : Controller
    {
        private ApplicationDbContext _db;
        public LessonController(ApplicationDbContext context)
        {
            _db = context;
        }

        public IActionResult CreateLesson(int moduleId)
        {
            var module = _db.Modules.FirstOrDefault(m => m.Id == moduleId);
            var model = CourseService.ComposeCourseModel(_db, module.CourseId);
            model.Module = module;
            return View(model);
        }
        
         public IActionResult EditLesson(int moduleId, int courseId, int lessonId)
         {
             var model = CourseService.ComposeCourseModel(_db, courseId, moduleId);
             var lesson = _db.Lessons.Find(lessonId);
             model.Lesson = lesson;
             return View("EditLesson", model);
         }       
        
         [HttpPost]
          public IActionResult EditLesson(StudyCourseViewModel model, int moduleId, int courseId, 
              int oldOrder, int lessonId)
          {
              _db.Lessons.Update(model.Lesson);
              model.Module = _db.Modules.Find(moduleId);
              model.Module.Lessons = _db.Lessons.Where(l => l.ModuleId == moduleId).ToList();
              model.Module.Assignments = _db.Assignments.Where(a => a.ModuleId == moduleId).ToList();
              LessonService.ResolveOrders(_db, model, oldOrder);
              _db.SaveChanges();
            return RedirectToAction("Study", "Course", new{courseId});
          }               

        [HttpPost]
        public IActionResult CreateLesson(StudyCourseViewModel model, int courseId, int moduleId)
        {
            var lesson = model.Lesson;
            lesson.ModuleId = moduleId;
            model = CourseService.ComposeCourseModel(_db, courseId, moduleId);
            model.Lesson = lesson;
            LessonService.ResolveOrders(_db, model, -1);
            _db.Lessons.Add(lesson);
            _db.SaveChanges();
            return RedirectToAction("Study", "Course", new{courseId});
        }

        public IActionResult ShowLesson(int courseId, int moduleId, int lessonId)
        {
            var model = CourseService.ComposeCourseModel(_db, courseId, moduleId);
            var lesson = _db.Lessons.Find(lessonId);
            model.Lesson = lesson;
            return View("ViewLesson", model);                     
        }

        public IActionResult DeleteLesson(StudyCourseViewModel model, int lessonId, int courseId)
        {
            var lesson = _db.Lessons.Find(lessonId);
            _db.Lessons.Remove(lesson);
            _db.SaveChanges();
            CourseService.FillModuleData(_db, model, lesson.ModuleId);
            CourseService.ResolveOrdersAfterDelete(_db, model, lesson.Order);
            return RedirectToAction("Study", "Course", new{courseId});
        }
    }
}