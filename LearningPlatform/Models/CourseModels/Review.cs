using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using LearningPlatform.Models.UserModels;

namespace LearningPlatform.Models.CourseModels
{
    [DataContract]
    public class Review 
    {
        [Key]
           [DataMember]
        public int Id { get; set; }
        [Required]
           [DataMember]
        public int StudentId { get; set; }

        public Student Student { get; set; }
        [Required]
           [DataMember]
        public int CourseId { get; set; }
        public Course Course { get; set; }
        
        [Required]
           [DataMember]
        public string Text { get; set; }
        [Required]
           [DataMember]
        public int Rank { get; set; }
        [Required]
           [DataMember]
        public DateTime DateTime { get; set; }
    } 
}