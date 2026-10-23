using BooksLibrary.Domain.Interfaces;
using BooksLibrary.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<IBookRepository, BookRepository>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://fakerestapi.azurewebsites.net/"));

builder.Services.AddHttpClient<IAuthorRepository, AuthorRepository>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://fakerestapi.azurewebsites.net/"));

builder.Services.AddCors(o => o.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
app.Run();