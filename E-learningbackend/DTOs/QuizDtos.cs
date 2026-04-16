namespace Elearningbackend.DTOs
{
    public class QuizDto
    {
        public int QuizId { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public int QuestionsCount { get; set; }
    }

    public class CreateQuizDto
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
    }

    public class QuizWithQuestionsDto
    {
        public int QuizId { get; set; }
        public string Title { get; set; }
        public List<QuestionDto> Questions { get; set; }
    }

    public class QuestionDto
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
    }

    public class CreateQuestionDto
    {
        public int QuizId { get; set; }
        public string QuestionText { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string CorrectAnswer { get; set; }
    }

    public class QuizSubmissionDto
    {
        public int QuizId { get; set; }
        public Dictionary<int, string> Answers { get; set; } // QuestionId -> SelectedAnswer
    }

    public class QuizResultDto
    {
        public int ResultId { get; set; }
        public int QuizId { get; set; }
        public string QuizTitle { get; set; }
        public int Score { get; set; }
        public int TotalQuestions { get; set; }
        public DateTime AttemptDate { get; set; }
    }
}