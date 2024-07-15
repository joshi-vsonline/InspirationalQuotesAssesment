using Microsoft.AspNetCore.Components;
using MudBlazor;
using Quotes.UI.DTO;
namespace Quotes.UI.Pages
{
   public partial class Home : ComponentBase
    {
        private bool isSearchExpanded = false;
        private List<Quotes.UI.DTO.QuoteDto> quotes = new();


        private string newTag = String.Empty;
        private List<string> currentTags = new();
        private List<String> tagsList = new List<string>();
        private QuoteForm quoteForm;
        private string searchAuthor { get; set; }
        private string searchTags { get; set; }
        private string searchContent { get; set; }

        private void OnAuthorChanged(string value)
        {
            searchAuthor = value;
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
            searchContent = value;
        }

        protected override async Task OnInitializedAsync()
        {
            quotes = (await QuotesApiService.GetAllQuotes()).ToList();
        }

        private async Task SearchQuotes()
        {
            quotes = (await QuotesApiService.SearchQuotes(searchAuthor, tagsList, searchContent)).ToList();
        }

        private void AddQuote()
        {
            NavigationManager.NavigateTo("/add-quote");
        }

        private async Task clearSearch()
        {
            searchAuthor = string.Empty;
            searchTags = string.Empty;
            searchContent = string.Empty;
            await LoadQuotes();
            isSearchExpanded = false;
            StateHasChanged();
        }

        private void EditQuote(int id)
        {
            NavigationManager.NavigateTo($"/edit-quote/{id}");
        }

        internal async Task LoadQuotes()
        {
            quotes = (await QuotesApiService.GetAllQuotes()).ToList();
        }

        private async Task DeleteQuote(int id)
        {
            await QuotesApiService.DeleteQuote(id);
            quotes = (await QuotesApiService.GetAllQuotes()).ToList();
        }

        private async Task ConfirmDeleteQuote(int id)
        {
            var parameters = new DialogParameters
    {
        { nameof(ConfirmDeleteDialog.ContentText), "Do you really want to delete this quote?" },
        { nameof(ConfirmDeleteDialog.ButtonText), "Delete" },
        { nameof(ConfirmDeleteDialog.Color), Color.Error }
    };

            var dialog = DialogService.Show<ConfirmDeleteDialog>("Confirm Delete", parameters);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                await DeleteQuote(id);
            }
        }

        private async Task ViewQuoteDetails(QuoteDto quote)
        {
            var parameters = new DialogParameters
    {
        { nameof(ViewQuoteDetailsDialog.Author), quote.Author },
        { nameof(ViewQuoteDetailsDialog.Tags), string.Join(", ", quote.Tags) },
        { nameof(ViewQuoteDetailsDialog.QuoteContent), quote.QuoteContent }
    };

            var dialog = DialogService.Show<ViewQuoteDetailsDialog>("Quote Details", parameters);
            await dialog.Result;
        }
    }
}
