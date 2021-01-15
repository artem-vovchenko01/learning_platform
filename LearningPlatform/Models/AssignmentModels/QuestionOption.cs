using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using LearningPlatform.Models.AnswerModels;

namespace LearningPlatform.Models.AssignmentModels
{
    [DataContract]
    public class QuestionOption 
    {
        [Key]
          [DataMember]
        public int Id { get; set; }
        [Required]
          [DataMember]
        public int QuestionId { get; set; }

          [DataMember]
        public string Text { get; set; }
        
        [Required]
          [DataMember]
        public bool Correct { get; set; }
        
        public Question Question { get; set; }
        public ICollection<AnswerQuestionOption> AnswerQuestionOptions { get; set; }
    } 
}
