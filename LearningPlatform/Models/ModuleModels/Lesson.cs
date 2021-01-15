using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace LearningPlatform.Models.ModuleModels
{
    [DataContract]
    public class Lesson 
    {
          [Key]
           [DataMember]
          public int Id { get; set; }
           [Required]
           [DataMember]
           public string Name { get; set; }         
          [Required]
           [DataMember]
          public int ModuleId { get; set; }        
          [Required]
           [DataMember]
          public int Order { get; set; }

 
          public Module Module { get; set; }       
        [Required]
           [DataMember]
        public string Content { get; set; }
    } 
}