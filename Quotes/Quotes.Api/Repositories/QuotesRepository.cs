using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Quotes.Api.Data;
using Quotes.Api.Models;
namespace Quotes.Api.Repositories
{
    public class QuotesRepository : IQuotesRepository
    {
        private readonly QuotesContext _context;

        public QuotesRepository(QuotesContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Quote>> GetAllAsync()
        {
           
            return await _context.Quotes.ToListAsync();
            
        }

        public async Task<Quote> GetByIdAsync(int id)
        {
            
            return await _context.Quotes.FindAsync(id);
        }

        public async Task AddAsync(Quote quote)
        {
            await _context.Quotes.AddAsync(quote);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Quote quote)
        {
            _context.Entry(quote).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var quote = await _context.Quotes.FindAsync(id);
            if (quote != null)
            {
                _context.Quotes.Remove(quote);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Quote>> SearchAsync(string author, List<string> tags, string quote)
        {
            var query = _context.Quotes.AsQueryable();

            if (!string.IsNullOrEmpty(author))
            {
                query = query.Where(t => t.Author.ToLower().Contains(author.ToLower()));
            }

            if (!string.IsNullOrEmpty(quote))
            {
                query = query.Where(t => t.QuoteContent.ToLower().Contains(quote.ToLower()));
            }

            var result = await query.ToListAsync();


            if (tags != null && tags.Count > 0)
            {
                var lowerTags = tags
                               .Where(tag => !string.IsNullOrEmpty(tag))
                               .Select(tag => tag.ToLower().Trim())
                               .ToList();
                
                if (lowerTags.Any(tag => tag.Contains(",")))
                {
                    lowerTags = lowerTags.SelectMany(tag => tag.Split(',')).ToList();
                }
                result = result.Where(t => t.Tags != null && t.Tags
                    .Split(',')
                    .Select(tag => tag.Trim().ToLower())
                    .Any(tag => lowerTags.Contains(tag)))
                    .ToList();
            }

            return result;
        }
    }
}
