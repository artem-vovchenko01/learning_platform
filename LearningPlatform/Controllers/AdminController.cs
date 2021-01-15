using System.Linq;
using LearningPlatform.Data;
using LearningPlatform.Services;
using LearningPlatform.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LearningPlatform.Controllers
{
    public class AdminController : Controller
    {
        
        private readonly ApplicationDbContext _db;

        public AdminController(ApplicationDbContext db)
        {
            _db = db;
        }
        
         public IActionResult Index()
         {
             var model = new AdminViewModel
             {
                 Students = _db.Students.ToList()
             };
             return View(model);
         }

         public IActionResult SerializeDb()
         {
             AdminService.ExportDbToXml(_db);
             return RedirectToAction("Index");
         }

         public IActionResult WipeDatabase()
         {
             AdminService.WipeDatabase(_db);
             return RedirectToAction("Index");
         }

         public IActionResult RestoreDatabase()
         {
             AdminService.RestoreDatabase(_db);
             return RedirectToAction("Index");
         }
    }
}