using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace LearningPlatform.Models.CourseModels
{
    [DataContract]
    public class CourseCategory  
    {
        [Key]
           [DataMember]
        public int Id { get; set; }
        [Required]
           [DataMember]
        public string Name { get; set; }
        
          [Required]
           [DataMember]
          public string CoverImage { get; set; }              
        
        public ICollection<Course> Courses { get; set; }

    } 
}
