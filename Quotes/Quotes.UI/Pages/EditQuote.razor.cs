using Microsoft.AspNetCore.Components;
using Quotes.UI.DTO;

namespace Quotes.UI.Pages
{
    public partial class EditQuote : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }
        private string tags;


        private string newTag = String.Empty;
        private List<string> currentTags = new();
        private List<String> tagsList;
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


        protected override async Task OnInitializedAsync()
        {
            if (Id != null)
            {
                var quoteDto = await QuotesApiService.GetQuoteById(Id);
                if (quoteDto != null)
                {
                    Author = quoteDto.Author;
                    tagsList = quoteDto.Tags;
                    QuoteContent = quoteDto.QuoteContent;
                }
            }
        }

        private async Task SaveQuote()
        {
            await quoteForm.ValidateForm();
            if (quoteForm.IsFormValid())
            {
                var updatedQuote = new CreateQuoteDto
                {
                    Author = Author,
                    QuoteContent = QuoteContent,
                    Tags = tagsList
                };

                if (Id != 0)
                {
                    await QuotesApiService.UpdateQuote(Id, updatedQuote);
                }

                Navigation.NavigateTo("/");
            }
        }

        private void GoToHome()
        {
            Navigation.NavigateTo("/");
        }
    }
}
