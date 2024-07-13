using Quotes.Api.Data;
using Quotes.Api.DTOs;
using Quotes.Api.Repositories;
using Quotes.Api.Models;

namespace Quotes.Api.Services
{
    public class QuotesServices : IQuotesServices
    {
        private readonly IQuotesRepository _quotesRepository;

        public QuotesServices(IQuotesRepository quotesRepository)
        {
            _quotesRepository = quotesRepository;

        }

        public async Task<IEnumerable<QuoteDto>> GetAllAsync()
        {
            var quotes = await _quotesRepository.GetAllAsync();
            return quotes.Select(t => new QuoteDto
            {
                Id = t.Id,
                Author = t.Author,
                Tags = t.Tags.Split(',').ToList(),
                QuoteContent = t.QuoteContent
            });
        }

        public async Task<QuoteDto> GetByIdAsync(int id)
        {
            var quote = await _quotesRepository.GetByIdAsync(id);
            if (quote == null)
                return null;

            return new QuoteDto
            {
                Id = quote.Id,
                Author = quote.Author,
                Tags = quote.Tags.Split(',').ToList(),
                QuoteContent = quote.QuoteContent
            };
        }

        public async Task AddAsync(IEnumerable<CreateQuoteDto> quotes)
        {
            foreach (var quote in quotes)
            {
                var quoteItem = new Quote
                {
                    Author = quote.Author,
                    Tags = string.Join(",", quote.Tags.Where(tag => !string.IsNullOrEmpty(tag))),
                    QuoteContent = quote.QuoteContent
                };
                await _quotesRepository.AddAsync(quoteItem);
            }
        }

        public async Task UpdateAsync(int id, CreateQuoteDto quote)
        {
            var existingQuote = await _quotesRepository.GetByIdAsync(id);
            if (existingQuote == null)
                return;

            existingQuote.Author = quote.Author;
            existingQuote.Tags = string.Join(",", quote.Tags.Where(tag => !string.IsNullOrEmpty(tag)));
            
            existingQuote.QuoteContent = quote.QuoteContent;

            await _quotesRepository.UpdateAsync(existingQuote);
        }

        public async Task DeleteAsync(int id)
        {
            await _quotesRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<QuoteDto>> SearchAsync(string author, List<string> tags, string quoteContent)
        {
            var quotes = await _quotesRepository.SearchAsync(author,tags,quoteContent);
            return quotes.Select(t => new QuoteDto
            {
                Id = t.Id,
                Author = t.Author,
                Tags = t.Tags.Split(',').ToList(),
                QuoteContent=t.QuoteContent
            });
        }

    }
}
