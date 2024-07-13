using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Quotes.UI;
using Quotes.UI.QuotesServices;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7141") });
builder.Services.AddMudServices();
builder.Services.AddScoped<IQuotesApiService, QuotesApiService>();

await builder.Build().RunAsync();

