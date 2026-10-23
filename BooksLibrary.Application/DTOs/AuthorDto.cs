namespace BooksLibrary.Application.DTOs;

public class AuthorDto
{
    public int Id { get; set; }
    public int IdBook { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string AssociatedBookInfo { get; set; } = string.Empty;
}