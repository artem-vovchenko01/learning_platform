using LearningPlatform.Models.CourseModels;
using Microsoft.AspNetCore.Http;

namespace LearningPlatform.ViewModels
{
    public class CreateCourseCategoryViewModel
    {
        public CourseCategory CourseCategory { get; set; }
        public IFormFile CoverPicture { get; set; }
    }
}