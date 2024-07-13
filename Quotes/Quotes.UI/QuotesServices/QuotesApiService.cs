
using System.Net.Http;
using System.Net.Http.Json;
using Quotes.UI.DTO;
namespace Quotes.UI.QuotesServices
{
    public class QuotesApiService : IQuotesApiService
    {
        private readonly HttpClient _httpClient;
        public QuotesApiService(HttpClient httpClient) {

            _httpClient = httpClient;
        }

        public async Task<IEnumerable<QuoteDto>> GetAllQuotes()
        {
            try
            {
                var quotes = (List<QuoteDto>)await _httpClient.GetFromJsonAsync<IEnumerable<QuoteDto>>("/api/Quotes");

                return quotes;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Failed to retrieve quotes", ex);

            }

        }

        public async Task<QuoteDto> GetQuoteById(int id)
        {

            try
            {
                return await _httpClient.GetFromJsonAsync<QuoteDto>($"/api/Quotes/{id}");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Failed to retrieve quote", ex);
            }
            
        }

        public async Task AddQuotes(IEnumerable<CreateQuoteDto> quotes)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/Quotes", quotes);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Failed to post the quotes", ex);
            }
        }

        
        public async Task UpdateQuote(int id, CreateQuoteDto quote)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"/api/Quotes/{id}", quote);
                response.EnsureSuccessStatusCode();
            }
            catch(HttpRequestException ex)
            {
                throw new Exception("Failed to update the quote", ex);
            }
            
        }


        public async Task DeleteQuote(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/Quotes/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch(HttpRequestException ex)
            {
                throw new Exception("Failed to delete the quote", ex);
            }

            
        }

        public async Task<IEnumerable<QuoteDto>> SearchQuotes(string author, List<string> tags, string quoteContent)
        {
            try
            {
                var query = new Dictionary<string, string>();
                if (!string.IsNullOrWhiteSpace(author)) query.Add("author", author);
                if (tags != null && tags.Count > 0) query.Add("tags", string.Join(",", tags));
                if (!string.IsNullOrWhiteSpace(quoteContent)) query.Add("quoteContent", quoteContent);

                var queryString = string.Join("&", query.Select(kvp => $"{kvp.Key}={kvp.Value}"));
                return await _httpClient.GetFromJsonAsync<IEnumerable<QuoteDto>>($"/api/Quotes/search?{queryString}");
            }
            catch(HttpRequestException ex)
            {
                throw new Exception("Failed to search quotes", ex);
            }
        }
    }
}
