using Elearningbackend.Data;
using Elearningbackend.Models;
using Microsoft.EntityFrameworkCore;

namespace Elearningbackend.Repositories
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task<IEnumerable<Course>> GetCoursesWithDetailsAsync();
        Task<Course> GetCourseWithDetailsAsync(int id);
    }

    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(ElearningDbContext context) : base(context) { }

        public async Task<IEnumerable<Course>> GetCoursesWithDetailsAsync()
        {
            return await _context.Courses
                .Include(c => c.Creator)
                .Include(c => c.Lessons)
                .Include(c => c.Quizzes)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Course> GetCourseWithDetailsAsync(int id)
        {
            return await _context.Courses
                .Include(c => c.Creator)
                .Include(c => c.Lessons)
                .Include(c => c.Quizzes)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CourseId == id);
        }
    }
}