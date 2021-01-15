using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using LearningPlatform.Models.AnswerModels;
using LearningPlatform.Models.ConnectionModels;

namespace LearningPlatform.Models.UserModels
{
    [DataContract]
    public class Student 
    {
          [Key]
          [DataMember]
          public int Id { get; set; }
          
          [Required]
          [DataMember]
          public string Name { get; set; }
          [Required]
          [DataMember]
          public string Surname { get; set; }
          [Required]
          [DataMember]
          public Country Country { get; set; }
          [Required]
          [DataMember]
          public int Age { get; set; }
          [Required]
          [DataMember]
          public Gender Gender { get; set; }
          [Required]
          [DataMember]
          public string Username { get; set; }
          [Required]
          [DataMember]
          public string Password { get; set; }
          [Required] 
          [DataMember]
          public DateTime RegistrationDate { get; set; }
         [Required]
            [DataMember]         
         public string ProfileImage { get; set; }


        public ICollection<AnswerAssignment> AnswerAssignments { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
    }
     public enum Gender { Male, Female, NoData } 
     public enum Country { Ukarine, Russia, USA }          
}
