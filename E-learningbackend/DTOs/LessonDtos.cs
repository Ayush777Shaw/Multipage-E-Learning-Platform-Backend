namespace Elearningbackend.DTOs
{
    public class LessonDto
    {
        public int LessonId { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int OrderIndex { get; set; }
    }

    public class CreateLessonDto
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int OrderIndex { get; set; }
    }

    public class UpdateLessonDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int OrderIndex { get; set; }
    }
}