using Quotes.Api.Models;

namespace Quotes.Api.Repositories
{
    public interface IQuotesRepository
    {

        Task<IEnumerable<Quote>> GetAllAsync();
        Task<Quote> GetByIdAsync(int id);
        Task AddAsync(Quote task);
        Task UpdateAsync(Quote task);
        Task DeleteAsync(int id);
        Task<IEnumerable<Quote>> SearchAsync(string author, List<string> tags, string quote);
    }
}
