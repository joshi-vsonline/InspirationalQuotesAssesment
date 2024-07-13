using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Quotes.Api.Data;
using Quotes.Api.Repositories;
using Quotes.Api.Services;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
builder.Services.AddScoped<IQuotesRepository, QuotesRepository>();
builder.Services.AddScoped<IQuotesServices, QuotesServices>();
builder.Services.AddControllers();
builder.Services.AddDbContext<QuotesContext>(options =>
        options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddCors(options => {
    options.AddPolicy("DevCorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();

    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("DevCorsPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
