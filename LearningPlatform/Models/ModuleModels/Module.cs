using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using LearningPlatform.Models.AssignmentModels;
using LearningPlatform.Models.CourseModels;

namespace LearningPlatform.Models.ModuleModels
{
    [DataContract]
    public class Module 
    {
        [Key]
           [DataMember]
        public int Id { get; set; }
        [Required]
           [DataMember]
        public string Name { get; set; }
        [Required]
           [DataMember]
        public int CourseId { get; set; }
        [Required]
           [DataMember]
        public string Description { get; set; }
        
        public Course Course { get; set; }
        public ICollection<Lesson> Lessons { get; set; }
        public ICollection<Assignment> Assignments { get; set; }
        
    } 
}