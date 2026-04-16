using Microsoft.AspNetCore.Mvc;
using Elearningbackend.DTOs;
using Elearningbackend.Services;
using Elearningbackend.Repositories;

namespace Elearningbackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionsController(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        [HttpPost]
        public async Task<ActionResult<QuestionDto>> CreateQuestion(CreateQuestionDto createQuestionDto)
        {
            try
            {
                var question = new Models.Question
                {
                    QuizId = createQuestionDto.QuizId,
                    QuestionText = createQuestionDto.QuestionText,
                    OptionA = createQuestionDto.OptionA,
                    OptionB = createQuestionDto.OptionB,
                    OptionC = createQuestionDto.OptionC,
                    OptionD = createQuestionDto.OptionD,
                    CorrectAnswer = createQuestionDto.CorrectAnswer
                };

                var createdQuestion = await _questionRepository.AddAsync(question);

                var questionDto = new QuestionDto
                {
                    QuestionId = createdQuestion.QuestionId,
                    QuestionText = createdQuestion.QuestionText,
                    OptionA = createdQuestion.OptionA,
                    OptionB = createdQuestion.OptionB,
                    OptionC = createdQuestion.OptionC,
                    OptionD = createdQuestion.OptionD
                };

                return CreatedAtAction(nameof(CreateQuestion), questionDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}