using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using LearningPlatform.Models.AssignmentModels;
using LearningPlatform.Models.UserModels;

namespace LearningPlatform.Models.AnswerModels
{
    [DataContract]
    public class AnswerThought
    {
           [Key]
            [DataMember]
            public int Id { get; set; }       
           [Required]
            [DataMember]
           public string Content { get; set; }
           [Required]
            [DataMember]
           public int Grade { get; set; }

           [Required]
            [DataMember]
           public int ThoughtId { get; set; }

           [Required]
            [DataMember]
           public int AnswerAssignmentId { get; set; }

           public AnswerAssignment AnswerAssignment { get; set; }
           public Thought Thought { get; set; }
    }
}