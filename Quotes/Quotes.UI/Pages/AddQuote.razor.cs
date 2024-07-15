using Microsoft.AspNetCore.Components;
using Quotes.UI.DTO;


namespace Quotes.UI.Pages
{
    public partial class AddQuote: ComponentBase
    {
        private CreateQuoteDto currentQuote = new();
        private List<CreateQuoteDto> quotes = new();

        private string newTag = String.Empty;
        private List<string> currentTags = new();
        private List<String> tagsList = new List<string>();
        private QuoteForm quoteForm;
        private string Author { get; set; }
        private string Tags { get; set; }
        private string QuoteContent { get; set; }


        private void OnAuthorChanged(string value)
        {
            Author = value;
        }

        private void OnTagsChanged(string value)
        {
            if (!tagsList.Contains(value))
            {
                tagsList.Add(value);
            }
            else
            {
                tagsList.Remove(value);
            }

        }

        private void OnQuoteContentChanged(string value)
        {
            QuoteContent = value;
        }

        private async Task AddNewQuote()
        {
            CreateQuoteDto newQuote = new();
            await quoteForm.ValidateForm();
            if (quoteForm.IsFormValid() && tagsList != null)
            {

                newQuote.Author = Author;
                newQuote.Tags = tagsList;
                newQuote.QuoteContent = QuoteContent;
                quoteForm.ResetValues();
                tagsList = new();
                quotes.Add(newQuote);
                StateHasChanged();
            }
        }

        // private async Task AddNewQuote()
        // {
        //     await form.Validate();
        //     if (form.IsValid)
        //     {
        //         currentQuote.Tags = new List<string>(currentTags);
        //         quotes.Add(currentQuote);
        //         currentQuote = new CreateQuoteDto(); 
        //         currentTags.Clear();
        //         newTag = string.Empty;
        //         StateHasChanged();
        //     }
        // }

        private void RemoveQuote(CreateQuoteDto quote)
        {
            quotes.Remove(quote);
            StateHasChanged();
        }

        private async Task SaveQuotes()
        {
            if (quotes.Count > 0)
            {
                await QuotesApiService.AddQuotes(quotes);
                Navigation.NavigateTo("/");
            }
        }

        private void RemoveTag(string tag)
        {
            currentTags.Remove(tag);
        }

        private void GoToHome()
        {
            Navigation.NavigateTo("/");
        }

    }
}
