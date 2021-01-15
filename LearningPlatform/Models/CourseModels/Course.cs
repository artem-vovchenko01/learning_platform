using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using LearningPlatform.Data;
using LearningPlatform.Models.ConnectionModels;
using LearningPlatform.Models.UserModels;

namespace LearningPlatform.Models.CourseModels
{
    [DataContract]
    public class Course
    {
        [Key]
           [DataMember]
        public int Id { get; set; }
           [DataMember]
        public int CourseCategoryId { get; set; }
        
        [Required]
           [DataMember]
        public string Name { get; set; }
        
        [Required]
           [DataMember]
        public string Description { get; set; }
        
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<ModuleModels.Module> Modules { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<CourseTeacher> CourseTeachers { get; set; }
        [Required]
        public CourseCategory CourseCategory { get; set; }

        [Required]
           [DataMember]
        public string Language { get; set; }
        [Required]
           [DataMember]
        public int RateToPass { get; set; }

        [Required]
           [DataMember]
        public int TimeToPass { get; set; }

        [Required]
           [DataMember]
        public string CoverImage { get; set; }

         [NotMapped]
         public double AverageReview { get; set; }
        }
    
}