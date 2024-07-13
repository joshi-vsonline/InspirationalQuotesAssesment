using Quotes.UI.DTO;

namespace Quotes.UI.QuotesServices
{
    public interface IQuotesApiService
    {
        Task<IEnumerable<QuoteDto>> GetAllQuotes();
        Task<QuoteDto> GetQuoteById(int id);
        Task AddQuotes(IEnumerable<CreateQuoteDto> quote);
        Task UpdateQuote(int id, CreateQuoteDto quote);
        Task DeleteQuote(int id);
        Task<IEnumerable<QuoteDto>> SearchQuotes(string author, List<string> tags, string quoteContent);
    }
}
