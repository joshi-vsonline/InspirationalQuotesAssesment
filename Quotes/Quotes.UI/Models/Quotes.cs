﻿using System.ComponentModel.DataAnnotations;

namespace Quotes.UI.Models
{
    public class QuoteModel
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
