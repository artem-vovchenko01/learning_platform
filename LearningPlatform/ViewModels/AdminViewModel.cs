using System.Collections.Generic;
using LearningPlatform.Models.UserModels;

namespace LearningPlatform.ViewModels
{
    public class AdminViewModel
    {
        public ICollection<Student> Students { get; set; }
    }
}