using Microsoft.AspNetCore.Mvc;
using Elearningbackend.DTOs;
using Elearningbackend.Services;

namespace Elearningbackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LessonsController : ControllerBase
    {
        private readonly ILessonService _lessonService;

        public LessonsController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LessonDto>>> GetLessonsByCourse([FromQuery] int courseId)
        {
            var lessons = await _lessonService.GetLessonsByCourseAsync(courseId);
            return Ok(lessons);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LessonDto>> GetLesson(int id)
        {
            try
            {
                var lesson = await _lessonService.GetLessonByIdAsync(id);
                return Ok(lesson);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<LessonDto>> CreateLesson(CreateLessonDto createLessonDto)
        {
            try
            {
                var lesson = await _lessonService.CreateLessonAsync(createLessonDto);
                return CreatedAtAction(nameof(GetLesson), new { id = lesson.LessonId }, lesson);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLesson(int id, UpdateLessonDto updateLessonDto)
        {
            try
            {
                var lesson = await _lessonService.UpdateLessonAsync(id, updateLessonDto);
                return Ok(lesson);
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
        public async Task<IActionResult> DeleteLesson(int id)
        {
            var deleted = await _lessonService.DeleteLessonAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}