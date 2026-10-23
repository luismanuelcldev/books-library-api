using BooksLibrary.Domain.Entities;

namespace BooksLibrary.Domain.Interfaces;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllAsync(CancellationToken ct);
    Task<Book?> GetByIdAsync(int id, CancellationToken ct);
    Task<Book> CreateAsync(Book book, CancellationToken ct);
    Task<Book> UpdateAsync(int id, Book book, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
}