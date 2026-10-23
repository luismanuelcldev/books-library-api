using BooksLibrary.Domain.Entities;

namespace BooksLibrary.Domain.Interfaces;

public interface IAuthorRepository
{
    Task<IEnumerable<Author>> GetAllAsync(CancellationToken ct);
    Task<Author?> GetByIdAsync(int id, CancellationToken ct);
    Task<Author> CreateAsync(Author author, CancellationToken ct);
    Task<Author> UpdateAsync(int id, Author author, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
    Task<IEnumerable<Author>> GetAuthorsByBookIdAsync(int bookId, CancellationToken ct);
}