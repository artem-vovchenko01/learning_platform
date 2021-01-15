using LearningPlatform.Models;
using LearningPlatform.Models.UserModels;

namespace LearningPlatform.Services
{
    public class StudentService
    {
            public static Student LoggedInStudent;
    
            public static bool IsAdmin => LoggedInStudent != null && LoggedInStudent.Username == "admin";

    }
}