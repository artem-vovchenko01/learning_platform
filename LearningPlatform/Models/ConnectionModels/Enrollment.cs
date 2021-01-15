using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using LearningPlatform.Models.CourseModels;
using LearningPlatform.Models.UserModels;

namespace LearningPlatform.Models.ConnectionModels
{
    [DataContract]
    public class Enrollment 
    {
           [DataMember]
        [Key]
        public int Id { get; set; }
        [Required]
           [DataMember]
        public int StudentId { get; set; }
        [Required]
           [DataMember]
        public int CourseId { get; set; }
        
        public Course Course { get; set; }
        public Student Student { get; set; }
        [Required]
        public DateTime EnrollDate { get; set; }
    } 
}