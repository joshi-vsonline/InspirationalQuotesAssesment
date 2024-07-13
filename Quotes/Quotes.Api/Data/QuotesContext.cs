using Microsoft.EntityFrameworkCore;
using Quotes.Api.Models;

namespace Quotes.Api.Data
{
    public class QuotesContext : DbContext
    {
        public QuotesContext(DbContextOptions<QuotesContext> options) : base(options) { }

        public DbSet<Quote> Quotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Quote>().HasKey(t => t.Id);
            modelBuilder.Entity<Quote>().Property(t => t.Id).ValueGeneratedOnAdd();
        }
    }
}
