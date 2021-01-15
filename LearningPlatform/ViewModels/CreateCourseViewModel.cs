using System.Collections.Generic;
using LearningPlatform.Models;
using LearningPlatform.Models.CourseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LearningPlatform.ViewModels
{
    public class CreateCourseViewModel
    {
        public SelectList CourseCategories { get; set; }
        public Course Course { get; set; }
        public IFormFile CoverPicture { get; set; }
    }
}