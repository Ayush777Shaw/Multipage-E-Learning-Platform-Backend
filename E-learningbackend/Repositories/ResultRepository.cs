using Elearningbackend.Data;
using Elearningbackend.Models;
using Microsoft.EntityFrameworkCore;

namespace Elearningbackend.Repositories
{
    public interface IResultRepository : IRepository<Result>
    {
        Task<IEnumerable<Result>> GetResultsByUserAsync(int userId);
        Task<Result> GetResultByUserAndQuizAsync(int userId, int quizId);
    }

    public class ResultRepository : Repository<Result>, IResultRepository
    {
        public ResultRepository(ElearningDbContext context) : base(context) { }

        public async Task<IEnumerable<Result>> GetResultsByUserAsync(int userId)
        {
            return await _context.Results
                .Where(r => r.UserId == userId)
                .Include(r => r.Quiz)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Result> GetResultByUserAndQuizAsync(int userId, int quizId)
        {
            return await _context.Results
                .FirstOrDefaultAsync(r => r.UserId == userId && r.QuizId == quizId);
        }
    }
}