using System.ComponentModel.DataAnnotations;

namespace Quotes.UI.DTO
{
    public class QuoteDto
    {
        public int Id { get; set; }
        public String Author { get; set; }

        public List<string> Tags { get; set; }

        public string QuoteContent { get; set; }
    }

    public class CreateQuoteDto
    {
        [Required]
        public String Author { get; set; }

        [Required]
        public List<string> Tags { get; set; }
        [Required]
        public string QuoteContent { get; set; }
    }
}
