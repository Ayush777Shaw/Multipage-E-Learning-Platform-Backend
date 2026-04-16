using Microsoft.AspNetCore.Mvc;
using Elearningbackend.DTOs;
using Elearningbackend.Services;

namespace Elearningbackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizzesController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizzesController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizDto>>> GetQuizzesByCourse([FromQuery] int courseId)
        {
            var quizzes = await _quizService.GetQuizzesByCourseAsync(courseId);
            return Ok(quizzes);
        }

        [HttpGet("{quizId}/questions")]
        public async Task<ActionResult<QuizWithQuestionsDto>> GetQuizWithQuestions(int quizId)
        {
            try
            {
                var quiz = await _quizService.GetQuizWithQuestionsAsync(quizId);
                return Ok(quiz);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<QuizDto>> CreateQuiz(CreateQuizDto createQuizDto)
        {
            try
            {
                var quiz = await _quizService.CreateQuizAsync(createQuizDto);
                return CreatedAtAction(nameof(GetQuizzesByCourse), new { courseId = quiz.CourseId }, quiz);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Course not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{quizId}/submit")]
        public async Task<ActionResult<QuizResultDto>> SubmitQuiz(int quizId, QuizSubmissionDto submissionDto)
        {
            try
            {
                submissionDto.QuizId = quizId;
                // In a real app, get user ID from authentication
                int userId = 1; // Placeholder
                var result = await _quizService.SubmitQuizAsync(userId, submissionDto);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuiz(int id)
        {
            var deleted = await _quizService.DeleteQuizAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}