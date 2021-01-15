using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using LearningPlatform.Models.UserModels;

namespace LearningPlatform.Models.AnswerModels
{
    [DataContract]
    public class AnswerAssignment
    {
            [Key]
            [DataMember]
            public int Id { get; set; }
 
         [Required]
            [DataMember]
         public DateTime LastTrial { get; set; }
         [Required]
            [DataMember]
         public int AttemptsTaken { get; set; }

         [Required]
            [DataMember]
         public int AssignmentId { get; set; }

         [Required]
            [DataMember]
         public int StudentId { get; set; }

         public Student Student { get; set; }
         public ICollection<AnswerQuestionOption> AnswerQuestionOptions { get; set; }
         public ICollection<AnswerThought> AnswerThoughts { get; set; }       
    }
}