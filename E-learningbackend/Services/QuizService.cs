using Elearningbackend.DTOs;
using Elearningbackend.Models;
using Elearningbackend.Repositories;

namespace Elearningbackend.Services
{
    public interface IQuizService
    {
        Task<IEnumerable<QuizDto>> GetQuizzesByCourseAsync(int courseId);
        Task<QuizWithQuestionsDto> GetQuizWithQuestionsAsync(int quizId);
        Task<QuizDto> CreateQuizAsync(CreateQuizDto createQuizDto);
        Task<bool> DeleteQuizAsync(int id);
        Task<QuizResultDto> SubmitQuizAsync(int userId, QuizSubmissionDto submissionDto);
        Task<IEnumerable<QuizResultDto>> GetUserResultsAsync(int userId);
    }

    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IResultRepository _resultRepository;
        private readonly ICourseRepository _courseRepository;

        public QuizService(
            IQuizRepository quizRepository,
            IQuestionRepository questionRepository,
            IResultRepository resultRepository,
            ICourseRepository courseRepository)
        {
            _quizRepository = quizRepository;
            _questionRepository = questionRepository;
            _resultRepository = resultRepository;
            _courseRepository = courseRepository;
        }

        public async Task<IEnumerable<QuizDto>> GetQuizzesByCourseAsync(int courseId)
        {
            var quizzes = await _quizRepository.GetQuizzesByCourseAsync(courseId);

            return quizzes.Select(q => new QuizDto
            {
                QuizId = q.QuizId,
                CourseId = q.CourseId,
                Title = q.Title,
                QuestionsCount = q.Questions?.Count ?? 0
            });
        }

        public async Task<QuizWithQuestionsDto> GetQuizWithQuestionsAsync(int quizId)
        {
            var quiz = await _quizRepository.GetQuizWithQuestionsAsync(quizId);
            if (quiz == null)
            {
                throw new KeyNotFoundException("Quiz not found");
            }

            return new QuizWithQuestionsDto
            {
                QuizId = quiz.QuizId,
                Title = quiz.Title,
                Questions = quiz.Questions.Select(q => new QuestionDto
                {
                    QuestionId = q.QuestionId,
                    QuestionText = q.QuestionText,
                    OptionA = q.OptionA,
                    OptionB = q.OptionB,
                    OptionC = q.OptionC,
                    OptionD = q.OptionD
                }).ToList()
            };
        }

        public async Task<QuizDto> CreateQuizAsync(CreateQuizDto createQuizDto)
        {
            // Verify course exists
            var course = await _courseRepository.GetByIdAsync(createQuizDto.CourseId);
            if (course == null)
            {
                throw new KeyNotFoundException("Course not found");
            }

            var quiz = new Quiz
            {
                CourseId = createQuizDto.CourseId,
                Title = createQuizDto.Title
            };

            var createdQuiz = await _quizRepository.AddAsync(quiz);

            return new QuizDto
            {
                QuizId = createdQuiz.QuizId,
                CourseId = createdQuiz.CourseId,
                Title = createdQuiz.Title,
                QuestionsCount = 0
            };
        }

        public async Task<bool> DeleteQuizAsync(int id)
        {
            var quiz = await _quizRepository.GetByIdAsync(id);
            if (quiz == null)
            {
                return false;
            }

            await _quizRepository.DeleteAsync(quiz);
            return true;
        }

        public async Task<QuizResultDto> SubmitQuizAsync(int userId, QuizSubmissionDto submissionDto)
        {
            var quiz = await _quizRepository.GetQuizWithQuestionsAsync(submissionDto.QuizId);
            if (quiz == null)
            {
                throw new KeyNotFoundException("Quiz not found");
            }

            int score = 0;
            var questions = quiz.Questions.ToList();

            foreach (var question in questions)
            {
                if (submissionDto.Answers.TryGetValue(question.QuestionId, out var answer))
                {
                    if (answer == question.CorrectAnswer)
                    {
                        score++;
                    }
                }
            }

            var result = new Result
            {
                UserId = userId,
                QuizId = submissionDto.QuizId,
                Score = score
            };

            await _resultRepository.AddAsync(result);

            return new QuizResultDto
            {
                ResultId = result.ResultId,
                QuizId = quiz.QuizId,
                QuizTitle = quiz.Title,
                Score = score,
                TotalQuestions = questions.Count,
                AttemptDate = result.AttemptDate
            };
        }

        public async Task<IEnumerable<QuizResultDto>> GetUserResultsAsync(int userId)
        {
            var results = await _resultRepository.GetResultsByUserAsync(userId);

            return results.Select(r => new QuizResultDto
            {
                ResultId = r.ResultId,
                QuizId = r.QuizId,
                QuizTitle = r.Quiz?.Title,
                Score = r.Score,
                TotalQuestions = r.Quiz?.Questions?.Count ?? 0,
                AttemptDate = r.AttemptDate
            });
        }
    }
}