using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quotes.Api.DTOs;
using Quotes.Api.Services;
using System.Threading.Tasks;

namespace Quotes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : ControllerBase
    {
        private readonly IQuotesServices _quotesService;

        public QuotesController(IQuotesServices quotesService)
        {
            _quotesService = quotesService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuoteDto>>> GetAll()
        {
            try {
                var quotes = await _quotesService.GetAllAsync();
                return Ok(quotes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuoteDto>> GetById(int id)
        {
            try
            {
                var quote = await _quotesService.GetByIdAsync(id);
                if (quote == null)
                    return NotFound();

                return Ok(quote);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] IEnumerable<CreateQuoteDto> quotes)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                
                await _quotesService.AddAsync(quotes);
                return CreatedAtAction(nameof(GetAll), null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateQuoteDto quote)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _quotesService.UpdateAsync(id, quote);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _quotesService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<QuoteDto>>> Search([FromQuery] string? Author, [FromQuery] List<string>? tags,  [FromQuery] string? quoteContent)
        {
            try
            {
                var tasks = await _quotesService.SearchAsync(Author, tags, quoteContent);
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
