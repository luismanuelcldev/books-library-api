namespace BooksLibrary.Infrastructure.External;

public class FakeApiBookModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int PageCount { get; set; }
    public DateTime PublishDate { get; set; }
}

public class FakeApiAuthorModel
{ 
    public int Id { get; set; }
    public int IdBook { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}
