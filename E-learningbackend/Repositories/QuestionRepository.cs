using Elearningbackend.Data;
using Elearningbackend.Models;
using Microsoft.EntityFrameworkCore;

namespace Elearningbackend.Repositories
{
    public interface IQuestionRepository : IRepository<Question>
    {
        Task<IEnumerable<Question>> GetQuestionsByQuizAsync(int quizId);
    }

    public class QuestionRepository : Repository<Question>, IQuestionRepository
    {
        public QuestionRepository(ElearningDbContext context) : base(context) { }

        public async Task<IEnumerable<Question>> GetQuestionsByQuizAsync(int quizId)
        {
            return await _context.Questions
                .Where(q => q.QuizId == quizId)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}