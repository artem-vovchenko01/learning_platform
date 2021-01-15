using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using LearningPlatform.Models.AssignmentModels;
using LearningPlatform.Models.UserModels;

namespace LearningPlatform.Models.AnswerModels
{
    [DataContract]
    public class AnswerQuestionOption
    {
        [Key]
            [DataMember]
        public int Id { get; set; }
        [Required]
            [DataMember]
        public bool Correct { get; set; }
        [Required]
            [DataMember]
        public int QuestionOptionId { get; set; }

        [Required]
            [DataMember]
        public int AnswerAssignmentId { get; set; }

        public QuestionOption QuestionOption { get; set; }
        public AnswerAssignment AnswerAssignment { get; set; }
    } 
}