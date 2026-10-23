using System.Net.Http.Json;
using BooksLibrary.Domain.Entities;
using BooksLibrary.Domain.Interfaces;
using BooksLibrary.Infrastructure.External;

namespace BooksLibrary.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly HttpClient _http;
    public BookRepository(HttpClient http) => _http = http;

    public async Task<IEnumerable<Book>> GetAllAsync(CancellationToken ct)
    {
        var res = await _http.GetFromJsonAsync<List<FakeApiBookModel>>("api/v1/Books", ct);
        return res?.Select(b => new Book { Id = b.Id, Title = b.Title, Description = b.Description, PageCount = b.PageCount, PublishDate = b.PublishDate }) ?? [];
    }

    public async Task<Book?> GetByIdAsync(int id, CancellationToken ct)
    {
        try
        {
            var b = await _http.GetFromJsonAsync<FakeApiBookModel>($"api/v1/Books/{id}", ct);
            return b is null ? null : new Book { Id = b.Id, Title = b.Title, Description = b.Description, PageCount = b.PageCount, PublishDate = b.PublishDate };
        }
        catch { return null; }
    }

    public async Task<Book> CreateAsync(Book book, CancellationToken ct)
    {
        var m = new FakeApiBookModel { Id = book.Id, Title = book.Title, Description = book.Description, PageCount = book.PageCount, PublishDate = book.PublishDate };
        await _http.PostAsJsonAsync("api/v1/Books", m, ct);
        return book;
    }

    public async Task<Book> UpdateAsync(int id, Book book, CancellationToken ct)
    {
        var m = new FakeApiBookModel { Id = book.Id, Title = book.Title, Description = book.Description, PageCount = book.PageCount, PublishDate = book.PublishDate };
        await _http.PutAsJsonAsync($"api/v1/Books/{id}", m, ct);
        return book;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        var res = await _http.DeleteAsync($"api/v1/Books/{id}", ct);
        return res.IsSuccessStatusCode;
    }
}