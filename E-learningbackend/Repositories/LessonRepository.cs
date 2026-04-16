using Elearningbackend.Data;
using Elearningbackend.Models;
using Microsoft.EntityFrameworkCore;

namespace Elearningbackend.Repositories
{
    public interface ILessonRepository : IRepository<Lesson>
    {
        Task<IEnumerable<Lesson>> GetLessonsByCourseAsync(int courseId);
    }

    public class LessonRepository : Repository<Lesson>, ILessonRepository
    {
        public LessonRepository(ElearningDbContext context) : base(context) { }

        public async Task<IEnumerable<Lesson>> GetLessonsByCourseAsync(int courseId)
        {
            return await _context.Lessons
                .Where(l => l.CourseId == courseId)
                .OrderBy(l => l.OrderIndex)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}