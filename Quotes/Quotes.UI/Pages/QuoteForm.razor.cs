using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using static MudBlazor.Colors;
 
namespace Quotes.UI.Pages
    {
        public partial class QuoteForm : ComponentBase
        {

            [Parameter]
            public string Author { get; set; }

            [Parameter]
            public EventCallback<string> AuthorChanged { get; set; }

            [Parameter]
            public string Tags { get; set; }

            [Parameter]
            public EventCallback<string> TagsChanged { get; set; }

            [Parameter]
            public string QuoteContent { get; set; }

            [Parameter]
            public EventCallback<string> QuoteContentChanged { get; set; }

            [Parameter] public bool EnableValidation { get; set; } = true;


            private List<string> currentTags = new List<string>();

            private List<string> tagsListParams;
            [Parameter]
            public List<string> TagsListParams
            {
                get => tagsListParams;
                set
                {
                    if (tagsListParams != value)
                    {
                        tagsListParams = value;
                        currentTags = new List<string>(tagsListParams);
                    }
                }
            }

            private async Task AddTag()
            {
                if (!string.IsNullOrWhiteSpace(Tags) && !currentTags.Contains(Tags))
                {
                    currentTags.Add(Tags.Trim());
                    await TagsChanged.InvokeAsync(Tags);
                    Tags = string.Empty;
                }
            }

            private async Task HandleKeyUp(KeyboardEventArgs args)
            {
                if (args.Key == "Enter")
                {
                    await AddTag();
                }
            }

            private async Task RemoveTag(string tag)
            {
                await TagsChanged.InvokeAsync(tag);
                currentTags.Remove(tag);
            }

            private MudForm form;

            private async Task HandleAuthorChanged(String value)
            {
                await AuthorChanged.InvokeAsync(value);
            }

            private async Task HandleQuoteContentChanged(String value)
            {
                await QuoteContentChanged.InvokeAsync(value);
            }

            public async Task ValidateForm()
            {
                await form.Validate();
            }

            public bool IsFormValid()
            {
                return form.IsValid;
            }

            public void ResetValues()
            {
                form.ResetAsync();
                currentTags = new();
            }
        }
    }


