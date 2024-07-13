using Quotes.Api.DTOs;

namespace Quotes.Api.Services
{
    public interface IQuotesServices
    {
        Task<IEnumerable<QuoteDto>> GetAllAsync();
        Task<QuoteDto> GetByIdAsync(int id);
        Task AddAsync(IEnumerable<CreateQuoteDto> quote);
        Task UpdateAsync(int id, CreateQuoteDto quote);
        Task DeleteAsync(int id);
        Task<IEnumerable<QuoteDto>> SearchAsync(string author, List<string> tags, string quoteContent);
    }
}
