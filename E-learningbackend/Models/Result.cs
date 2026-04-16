using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elearningbackend.Models
{
    public class Result
    {
        [Key]
        public int ResultId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int QuizId { get; set; }

        [Required]
        public int Score { get; set; }

        public DateTime AttemptDate { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("QuizId")]
        public Quiz Quiz { get; set; }
    }
}