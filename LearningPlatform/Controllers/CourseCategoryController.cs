using System;
using System.Collections.Generic;
using System.IO;
using LearningPlatform.Data;
using LearningPlatform.Models.CourseModels;
using LearningPlatform.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace LearningPlatform.Controllers
{
    public class CourseCategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment webHostEnvironment;  

        public CourseCategoryController(ApplicationDbContext db, IWebHostEnvironment hostEnvironment )
        {
            _db = db;
            webHostEnvironment = hostEnvironment;
        }
        
        // GET
        public IActionResult Index()
        {
            IEnumerable<CourseCategory> categories = _db.CourseCategories;
            return View(categories);
        }
        
        // GET - CREATE
         public IActionResult Create()
         {
             var model = new CreateCourseCategoryViewModel
             {
                 CourseCategory = new CourseCategory(),
             };
             return View(model);
         }       
        
         // POST - CREATE
         [HttpPost]
         [ValidateAntiForgeryToken]
          public IActionResult Create(CreateCourseCategoryViewModel model)
          {
              var fileName = UploadedFile(model);
              model.CourseCategory.CoverImage = fileName;
              _db.CourseCategories.Add(model.CourseCategory);
              _db.SaveChanges();
              return RedirectToAction("Index");
          }
          
           private string UploadedFile(CreateCourseCategoryViewModel model)  
           {  
               string uniqueFileName = null;  
   
               if (model.CoverPicture != null)  
               {  
                   var uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");  
                   uniqueFileName = Guid.NewGuid().ToString() + "_" + model.CoverPicture.FileName;  
                   var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                   using var fileStream = new FileStream(filePath, FileMode.Create);
                   model.CoverPicture.CopyTo(fileStream);
               }  
               return uniqueFileName;  
           }             
          
         // GET - EDIT
          public IActionResult Edit(int? id)
          {
              if (id == null || id == 0)
              {
                  return NotFound();
              }

              var courseCategory = _db.CourseCategories.Find(id);
              if (courseCategory == null) return NotFound();
              var model = new CreateCourseCategoryViewModel()
              {
                  CourseCategory = courseCategory
              };
              
              return View(model);
          }                      
          
          // POST - EDIT
          [HttpPost]
          [ValidateAntiForgeryToken]
           public IActionResult Edit(CreateCourseCategoryViewModel model)
           {
               var fileName = UploadedFile(model);
               if (fileName != null) model.CourseCategory.CoverImage = fileName;
               else model.CourseCategory.CoverImage = Request.Form["oldPicture"];
               if (ModelState.IsValid)
               {
                   _db.CourseCategories.Update(model.CourseCategory);
                   _db.SaveChanges();
                   return RedirectToAction("Index");
               }

               return RedirectToAction("Index");
           }     
           
          // GET - DELETE 
           public IActionResult Delete(int? id)
           {
               if (id == null || id == 0)
               {
                   return NotFound();
               }
 
               var obj = _db.CourseCategories.Find(id);
               if (obj == null) return NotFound();
               
               return View(obj);
           }                      
           
           // POST - DELETE
           [HttpPost]
           [ValidateAntiForgeryToken]
            public IActionResult DeletePost(int? id)
            {
                var obj = _db.CourseCategories.Find(id);
                if (obj == null)
                {
                    return NotFound();
                }
                _db.CourseCategories.Remove(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }          
    }
}