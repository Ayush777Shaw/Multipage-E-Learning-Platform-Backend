using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elearningbackend.Models
{
    public class Quiz
    {
        [Key]
        public int QuizId { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        // Navigation properties
        [ForeignKey("CourseId")]
        public Course Course { get; set; }

        public ICollection<Question> Questions { get; set; }
        public ICollection<Result> Results { get; set; }
    }
}