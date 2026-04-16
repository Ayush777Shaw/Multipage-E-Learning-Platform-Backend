using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elearningbackend.Models
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }

        [Required]
        public int QuizId { get; set; }

        [Required]
        public string QuestionText { get; set; }

        [Required]
        [StringLength(500)]
        public string OptionA { get; set; }

        [Required]
        [StringLength(500)]
        public string OptionB { get; set; }

        [Required]
        [StringLength(500)]
        public string OptionC { get; set; }

        [Required]
        [StringLength(500)]
        public string OptionD { get; set; }

        [Required]
        [StringLength(1)]
        public string CorrectAnswer { get; set; } // A, B, C, or D

        // Navigation property
        [ForeignKey("QuizId")]
        public Quiz Quiz { get; set; }
    }
}