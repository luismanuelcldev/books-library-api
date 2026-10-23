using System.Net.Http.Json;
using BooksLibrary.Domain.Entities;
using BooksLibrary.Domain.Interfaces;
using BooksLibrary.Infrastructure.External;

namespace BooksLibrary.Infrastructure.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly HttpClient _http;
    public AuthorRepository(HttpClient http) => _http = http;

    public async Task<IEnumerable<Author>> GetAllAsync(CancellationToken ct)
    {
        var res = await _http.GetFromJsonAsync<List<FakeApiAuthorModel>>("api/v1/Authors", ct);
        return res?.Select(a => new Author { Id = a.Id, IdBook = a.IdBook, FirstName = a.FirstName, LastName = a.LastName }) ?? [];
    }

    public async Task<Author?> GetByIdAsync(int id, CancellationToken ct)
    {
        try
        {
            var a = await _http.GetFromJsonAsync<FakeApiAuthorModel>($"api/v1/Authors/{id}", ct);
            return a == null ? null : new Author { Id = a.Id, IdBook = a.IdBook, FirstName = a.FirstName, LastName = a.LastName };
        }
        catch { return null; }
    }

    public async Task<Author> CreateAsync(Author author, CancellationToken ct)
    {
        var m = new FakeApiAuthorModel { Id = author.Id, IdBook = author.IdBook, FirstName = author.FirstName, LastName = author.LastName };
        await _http.PostAsJsonAsync("api/v1/Authors", m, ct);
        return author;
    }

    public async Task<Author> UpdateAsync(int id, Author author, CancellationToken ct)
    {
        var m = new FakeApiAuthorModel { Id = id, IdBook = author.IdBook, FirstName = author.FirstName, LastName = author.LastName };
        await _http.PutAsJsonAsync($"api/v1/Authors/{id}", m, ct);
        return author;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        var res = await _http.DeleteAsync($"api/v1/Authors/{id}", ct);
        return res.IsSuccessStatusCode;
    }

    public async Task<IEnumerable<Author>> GetAuthorsByBookIdAsync(int bookId, CancellationToken ct)
    {
        try
        {
            var res = await _http.GetFromJsonAsync<List<FakeApiAuthorModel>>($"api/v1/Authors/authors/books/{bookId}", ct);
            return res?.Select(a => new Author { Id = a.Id, IdBook = a.IdBook, FirstName = a.FirstName, LastName = a.LastName }) ?? [];
        }
        catch { return []; }
    }
}