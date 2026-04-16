using Elearningbackend.DTOs;
using Elearningbackend.Models;
using Elearningbackend.Repositories;

namespace Elearningbackend.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseDto>> GetAllCoursesAsync();
        Task<CourseDto> GetCourseByIdAsync(int id);
        Task<CourseDto> CreateCourseAsync(CreateCourseDto createCourseDto, int createdBy);
        Task<CourseDto> UpdateCourseAsync(int id, UpdateCourseDto updateCourseDto);
        Task<bool> DeleteCourseAsync(int id);
    }

    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
        {
            var courses = await _courseRepository.GetCoursesWithDetailsAsync();

            return courses.Select(c => new CourseDto
            {
                CourseId = c.CourseId,
                Title = c.Title,
                Description = c.Description,
                CreatedBy = c.CreatedBy,
                CreatorName = c.Creator?.FullName,
                CreatedAt = c.CreatedAt,
                LessonsCount = c.Lessons?.Count ?? 0,
                QuizzesCount = c.Quizzes?.Count ?? 0
            });
        }

        public async Task<CourseDto> GetCourseByIdAsync(int id)
        {
            var course = await _courseRepository.GetCourseWithDetailsAsync(id);
            if (course == null)
            {
                throw new KeyNotFoundException("Course not found");
            }

            return new CourseDto
            {
                CourseId = course.CourseId,
                Title = course.Title,
                Description = course.Description,
                CreatedBy = course.CreatedBy,
                CreatorName = course.Creator?.FullName,
                CreatedAt = course.CreatedAt,
                LessonsCount = course.Lessons?.Count ?? 0,
                QuizzesCount = course.Quizzes?.Count ?? 0
            };
        }

        public async Task<CourseDto> CreateCourseAsync(CreateCourseDto createCourseDto, int createdBy)
        {
            var course = new Course
            {
                Title = createCourseDto.Title,
                Description = createCourseDto.Description,
                CreatedBy = createdBy
            };

            var createdCourse = await _courseRepository.AddAsync(course);

            return new CourseDto
            {
                CourseId = createdCourse.CourseId,
                Title = createdCourse.Title,
                Description = createdCourse.Description,
                CreatedBy = createdCourse.CreatedBy,
                CreatedAt = createdCourse.CreatedAt,
                LessonsCount = 0,
                QuizzesCount = 0
            };
        }

        public async Task<CourseDto> UpdateCourseAsync(int id, UpdateCourseDto updateCourseDto)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
            {
                throw new KeyNotFoundException("Course not found");
            }

            course.Title = updateCourseDto.Title;
            course.Description = updateCourseDto.Description;

            await _courseRepository.UpdateAsync(course);

            var updatedCourse = await _courseRepository.GetCourseWithDetailsAsync(id);

            return new CourseDto
            {
                CourseId = updatedCourse.CourseId,
                Title = updatedCourse.Title,
                Description = updatedCourse.Description,
                CreatedBy = updatedCourse.CreatedBy,
                CreatorName = updatedCourse.Creator?.FullName,
                CreatedAt = updatedCourse.CreatedAt,
                LessonsCount = updatedCourse.Lessons?.Count ?? 0,
                QuizzesCount = updatedCourse.Quizzes?.Count ?? 0
            };
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
            {
                return false;
            }

            await _courseRepository.DeleteAsync(course);
            return true;
        }
    }
}