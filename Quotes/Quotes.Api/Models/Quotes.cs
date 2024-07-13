using Microsoft.Extensions.Primitives;
using System.ComponentModel.DataAnnotations;

namespace Quotes.Api.Models
{
    public class Quote
    {
        [Key]
        
        public int Id { get; set; }
        [Required]
        public String Author { get; set; }
        [Required]

        public string Tags { get; set; }
        [Required]
        public string QuoteContent { get; set; }
        
    }
}
