using BooksLibrary.Application.DTOs;
using BooksLibrary.Domain.Entities;
using BooksLibrary.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BooksLibrary.Api.Controllers;

[ApiController]
[Route("api/books")]
public class BooksController : ControllerBase
{
    private readonly IBookRepository _bookRepo;
    private readonly IAuthorRepository _authorRepo;

    public BooksController(IBookRepository bookRepo, IAuthorRepository authorRepo)
    {
        _bookRepo = bookRepo;
        _authorRepo = authorRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var books = await _bookRepo.GetAllAsync(ct);
        var dtos = books.Select(b => MapToDto(b));
        return Ok(dtos);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var book = await _bookRepo.GetByIdAsync(id, ct);
        if (book is null) return NotFound();

        var authors = await _authorRepo.GetAuthorsByBookIdAsync(id, ct);
        return Ok(MapToDto(book, authors));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BookDto dto, CancellationToken ct)
    {
        var book = MapToEntity(dto);
        var created = await _bookRepo.CreateAsync(book, ct);
        var response = MapToDto(created);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] BookDto dto, CancellationToken ct)
    {
        var existing = await _bookRepo.GetByIdAsync(id, ct);
        if (existing is null) return NotFound();

        var updated = await _bookRepo.UpdateAsync(id, MapToEntity(dto, id), ct);
        return Ok(MapToDto(updated));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var existing = await _bookRepo.GetByIdAsync(id, ct);
        if (existing is null) return NotFound();

        return await _bookRepo.DeleteAsync(id, ct) ? NoContent() : BadRequest();
    }

    private static BookDto MapToDto(Book book, IEnumerable<Author>? authors = null) => new()
    {
        Id = book.Id,
        Title = book.Title,
        Description = book.Description,
        PageCount = book.PageCount,
        PublishDate = book.PublishDate,
        Authors = authors?.Select(a => new AuthorDto
        {
            Id = a.Id,
            IdBook = a.IdBook,
            FirstName = a.FirstName,
            LastName = a.LastName
        }).ToList() ?? []
    };

    private static Book MapToEntity(BookDto dto, int? id = null) => new()
    {
        Id = id ?? dto.Id,
        Title = dto.Title,
        Description = dto.Description,
        PageCount = dto.PageCount,
        PublishDate = dto.PublishDate
    };
}
