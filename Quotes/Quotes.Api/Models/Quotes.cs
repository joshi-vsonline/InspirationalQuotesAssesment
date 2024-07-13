using Microsoft.Extensions.Primitives;
using System.ComponentModel.DataAnnotations;

namespace Quotes.Api.Models
{
    public class Quotes
    {
        [Key]
        public int Id { get; set; }
        public String Author { get; set; }

        public List<string> Tags { get; set; }

        public string QuoteContent { get; set; }
    }
}
