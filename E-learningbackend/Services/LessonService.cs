using Elearningbackend.DTOs;
using Elearningbackend.Models;
using Elearningbackend.Repositories;

namespace Elearningbackend.Services
{
    public interface ILessonService
    {
        Task<IEnumerable<LessonDto>> GetLessonsByCourseAsync(int courseId);
        Task<LessonDto> GetLessonByIdAsync(int id);
        Task<LessonDto> CreateLessonAsync(CreateLessonDto createLessonDto);
        Task<LessonDto> UpdateLessonAsync(int id, UpdateLessonDto updateLessonDto);
        Task<bool> DeleteLessonAsync(int id);
    }

    public class LessonService : ILessonService
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly ICourseRepository _courseRepository;

        public LessonService(ILessonRepository lessonRepository, ICourseRepository courseRepository)
        {
            _lessonRepository = lessonRepository;
            _courseRepository = courseRepository;
        }

        public async Task<IEnumerable<LessonDto>> GetLessonsByCourseAsync(int courseId)
        {
            var lessons = await _lessonRepository.GetLessonsByCourseAsync(courseId);

            return lessons.Select(l => new LessonDto
            {
                LessonId = l.LessonId,
                CourseId = l.CourseId,
                Title = l.Title,
                Content = l.Content,
                OrderIndex = l.OrderIndex
            });
        }

        public async Task<LessonDto> GetLessonByIdAsync(int id)
        {
            var lesson = await _lessonRepository.GetByIdAsync(id);
            if (lesson == null)
            {
                throw new KeyNotFoundException("Lesson not found");
            }

            return new LessonDto
            {
                LessonId = lesson.LessonId,
                CourseId = lesson.CourseId,
                Title = lesson.Title,
                Content = lesson.Content,
                OrderIndex = lesson.OrderIndex
            };
        }

        public async Task<LessonDto> CreateLessonAsync(CreateLessonDto createLessonDto)
        {
            // Verify course exists
            var course = await _courseRepository.GetByIdAsync(createLessonDto.CourseId);
            if (course == null)
            {
                throw new KeyNotFoundException("Course not found");
            }

            var lesson = new Lesson
            {
                CourseId = createLessonDto.CourseId,
                Title = createLessonDto.Title,
                Content = createLessonDto.Content,
                OrderIndex = createLessonDto.OrderIndex
            };

            var createdLesson = await _lessonRepository.AddAsync(lesson);

            return new LessonDto
            {
                LessonId = createdLesson.LessonId,
                CourseId = createdLesson.CourseId,
                Title = createdLesson.Title,
                Content = createdLesson.Content,
                OrderIndex = createdLesson.OrderIndex
            };
        }

        public async Task<LessonDto> UpdateLessonAsync(int id, UpdateLessonDto updateLessonDto)
        {
            var lesson = await _lessonRepository.GetByIdAsync(id);
            if (lesson == null)
            {
                throw new KeyNotFoundException("Lesson not found");
            }

            lesson.Title = updateLessonDto.Title;
            lesson.Content = updateLessonDto.Content;
            lesson.OrderIndex = updateLessonDto.OrderIndex;

            await _lessonRepository.UpdateAsync(lesson);

            return new LessonDto
            {
                LessonId = lesson.LessonId,
                CourseId = lesson.CourseId,
                Title = lesson.Title,
                Content = lesson.Content,
                OrderIndex = lesson.OrderIndex
            };
        }

        public async Task<bool> DeleteLessonAsync(int id)
        {
            var lesson = await _lessonRepository.GetByIdAsync(id);
            if (lesson == null)
            {
                return false;
            }

            await _lessonRepository.DeleteAsync(lesson);
            return true;
        }
    }
}