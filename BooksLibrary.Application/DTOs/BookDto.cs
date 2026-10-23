namespace BooksLibrary.Application.DTOs;

public class BookDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int PageCount {get; set; }
    public DateTime PublishDate { get; set; }
    public IEnumerable<AuthorDto> Authors { get; set; } = new List<AuthorDto>();

}
