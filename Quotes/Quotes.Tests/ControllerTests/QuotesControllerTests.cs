using Microsoft.AspNetCore.Mvc;
using Moq;
using Quotes.Api.Controllers;
using Quotes.Api.DTOs;
using Quotes.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quotes.Tests.ControllerTests
{
    public class QuotesControllerTests
    {
        private readonly Mock<IQuotesServices> _quotesServiceMock;
        private readonly QuotesController _quotesController;

        public QuotesControllerTests()
        {
            _quotesServiceMock = new Mock<IQuotesServices>();
            _quotesController = new QuotesController(_quotesServiceMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithListOfQuotes()
        {
            
            var quotes = new List<QuoteDto> { new QuoteDto { Id = 1, Author = "Author1", Tags = new List<string> { "tag1" }, QuoteContent = "Content1" } };
            _quotesServiceMock.Setup(service => service.GetAllAsync()).ReturnsAsync(quotes);

            
            var result = await _quotesController.GetAll();

            
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<QuoteDto>>(okResult.Value);
            Assert.Equal(quotes.Count, returnValue.Count);
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WithQuote()
        {
            
            var quote = new QuoteDto { Id = 1, Author = "Author1", Tags = new List<string> { "tag1" }, QuoteContent = "Content1" };
            _quotesServiceMock.Setup(service => service.GetByIdAsync(1)).ReturnsAsync(quote);

            
            var result = await _quotesController.GetById(1);

            
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<QuoteDto>(okResult.Value);
            Assert.Equal(quote.Id, returnValue.Id);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenQuoteDoesNotExist()
        {
            
            _quotesServiceMock.Setup(service => service.GetByIdAsync(1)).ReturnsAsync((QuoteDto)null);

            
            var result = await _quotesController.GetById(1);

            
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Add_ReturnsCreatedAtActionResult_WhenQuotesAreValid()
        {
            
            var quotes = new List<CreateQuoteDto> { new CreateQuoteDto { Author = "Author1", Tags = new List<string> { "tag1" }, QuoteContent = "Content1" } };

            
            var result = await _quotesController.Add(quotes);

            
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(_quotesController.GetAll), createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task Add_ReturnsBadRequest_WhenQuotesAreInvalid()
        {
            
            var quotes = new List<CreateQuoteDto> { new CreateQuoteDto { Author = null, Tags = null, QuoteContent = null } };
            _quotesController.ModelState.AddModelError("Author", "Required");
            _quotesController.ModelState.AddModelError("Tags", "Required");
            _quotesController.ModelState.AddModelError("QuoteContent", "Required");

            
            var result = await _quotesController.Add(quotes);

            
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public async Task Update_ReturnsNoContentResult_WhenQuoteIsValid()
        {
            
            var quote = new CreateQuoteDto { Author = "Author1", Tags = new List<string> { "tag1" }, QuoteContent = "Content1" };

            
            var result = await _quotesController.Update(1, quote);

            
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_WhenQuoteIsInvalid()
        {
            
            var quote = new CreateQuoteDto { Author = null, Tags = null, QuoteContent = null };
            _quotesController.ModelState.AddModelError("Author", "Required");
            _quotesController.ModelState.AddModelError("Tags", "Required");
            _quotesController.ModelState.AddModelError("QuoteContent", "Required");

            
            var result = await _quotesController.Update(1, quote);

            
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public async Task Delete_ReturnsNoContentResult_WhenQuoteIsDeleted()
        {
            
            var result = await _quotesController.Delete(1);

            
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Search_ReturnsOkResult_WithListOfQuotes()
        {
            
            var quotes = new List<QuoteDto> { new QuoteDto { Id = 1, Author = "Author1", Tags = new List<string> { "tag1" }, QuoteContent = "Content1" } };
            _quotesServiceMock.Setup(service => service.SearchAsync("Author1", null, null)).ReturnsAsync(quotes);

            
            var result = await _quotesController.Search("Author1", null, null);

            
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<QuoteDto>>(okResult.Value);
            Assert.Equal(quotes.Count, returnValue.Count);
        }

    }
}
