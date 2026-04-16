using Elearningbackend.Data;
using Elearningbackend.Models;
using Microsoft.EntityFrameworkCore;

namespace Elearningbackend.Repositories
{
    public interface IQuizRepository : IRepository<Quiz>
    {
        Task<IEnumerable<Quiz>> GetQuizzesByCourseAsync(int courseId);
        Task<Quiz> GetQuizWithQuestionsAsync(int quizId);
    }

    public class QuizRepository : Repository<Quiz>, IQuizRepository
    {
        public QuizRepository(ElearningDbContext context) : base(context) { }

        public async Task<IEnumerable<Quiz>> GetQuizzesByCourseAsync(int courseId)
        {
            return await _context.Quizzes
                .Where(q => q.CourseId == courseId)
                .Include(q => q.Questions)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Quiz> GetQuizWithQuestionsAsync(int quizId)
        {
            return await _context.Quizzes
                .Include(q => q.Questions)
                .AsNoTracking()
                .FirstOrDefaultAsync(q => q.QuizId == quizId);
        }
    }
}