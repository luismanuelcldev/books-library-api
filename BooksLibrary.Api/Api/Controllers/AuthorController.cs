using BooksLibrary.Application.DTOs;
using BooksLibrary.Domain.Entities;
using BooksLibrary.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BooksLibrary.Api.Controllers;

[ApiController]
[Route("api/authors")]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorRepository _authorRepo;
    public AuthorsController(IAuthorRepository authorRepo) => _authorRepo = authorRepo;

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var authors = await _authorRepo.GetAllAsync(ct);
        var dtos = authors.Select(a => new AuthorDto
        {
            Id = a.Id,
            IdBook = a.IdBook,
            FirstName = a.FirstName,
            LastName = a.LastName,
            AssociatedBookInfo = $"Linked to Book ID: {a.IdBook}"
        });
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var a = await _authorRepo.GetByIdAsync(id, ct);
        return a == null ? NotFound() : Ok(new AuthorDto { Id = a.Id, IdBook = a.IdBook, FirstName = a.FirstName, LastName = a.LastName });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Author author, CancellationToken ct) => Ok(await _authorRepo.CreateAsync(author, ct));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Author author, CancellationToken ct) => Ok(await _authorRepo.UpdateAsync(id, author, ct));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct) => await _authorRepo.DeleteAsync(id, ct) ? NoContent() : BadRequest();
}