using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using LearningPlatform.Models.ModuleModels;

namespace LearningPlatform.Models.AssignmentModels
{
    [DataContract]
    public class Assignment 
    {
           [Key]
            [DataMember]
           public int Id { get; set; }
            [Required]
            [DataMember]
            public string Name { get; set; }         
         [Required]
            [DataMember]
         public string Description { get; set; }           
           [Required]
            [DataMember]
           public int ModuleId { get; set; }        
           [Required]
            [DataMember]
           public int Order { get; set; }
  
           public Module Module { get; set; }           

        [Required]
            [DataMember]
        public int Duration { get; set; }
        [Required]
            [DataMember]
        public int RateToPass { get; set; }
        [Required]
            [DataMember]
        public int MaxTrials { get; set; }
        
        public ICollection<Question> Questions { get; set; }
        public ICollection<Thought> Thoughts { get; set; }
    } 
}