using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using LearningPlatform.Models.CourseModels;
using LearningPlatform.Models.UserModels;

namespace LearningPlatform.Models.ConnectionModels
{
    [DataContract]
    public class CourseTeacher
    {
        [Key]
           [DataMember]
        public int Id { get; set; }
        [Required]
           [DataMember]
        public int TeacherId { get; set; }
        [Required]
           [DataMember]
        public int CourseId { get; set; }
        
        
        public Teacher Teacher { get; set; }
        public Course Course { get; set; }
    }
}