namespace Elearningbackend.DTOs
{
    public class CourseDto
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public string CreatorName { get; set; }
        public DateTime CreatedAt { get; set; }
        public int LessonsCount { get; set; }
        public int QuizzesCount { get; set; }
    }

    public class CreateCourseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class UpdateCourseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}