using LearningPlatform.Models.UserModels;
using Microsoft.AspNetCore.Http;

namespace LearningPlatform.ViewModels
{
    public class CreateStudentViewModel
    {
        public Student Student { get; set; }
        public IFormFile CoverPicture { get; set; }
    }
}