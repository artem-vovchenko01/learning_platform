using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using LearningPlatform.Models.AnswerModels;

namespace LearningPlatform.Models.AssignmentModels
{
    [DataContract]
    public class Thought 
    {
           [Key]
           [DataMember]
           public int Id { get; set; }       
           [Required]
           [DataMember]
           public DateTime CreationDate { get; set; }          
           [Required]
           [DataMember]
          public string Name { get; set; }

          [Required]
           [DataMember]
          public string Content { get; set; }
          [Required]
           [DataMember]
          public int MaxGrade { get; set; }
          [Required]
           [DataMember]
          public int AssignmentId { get; set; }
          
          public Assignment Assignment { get; set; }            
          public ICollection<AnswerThought> AnswerThoughts { get; set; }
    }
}