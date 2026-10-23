# Books Library API

REST API built with C# and ASP.NET Core Web API that acts as a proxy between a frontend application and [FakeRestAPI](https://fakerestapi.azurewebsites.net/index.html). The project follows Clean Architecture principles to keep the domain, application, infrastructure, and presentation layers separated.

## Tech Stack

- C# / .NET 10
- ASP.NET Core Web API
- Clean Architecture
- Typed `HttpClient` integration with FakeRestAPI
- Swagger / OpenAPI

## Architecture

The solution is organized into four projects:

- **BooksLibrary.Domain**: Domain entities (`Book`, `Author`) and repository contracts.
- **BooksLibrary.Application**: Data Transfer Objects (DTOs) used by the API.
- **BooksLibrary.Infrastructure**: Repositories that consume FakeRestAPI through a typed `HttpClient`.
- **BooksLibrary.Api**: Controllers, dependency injection, CORS, and Swagger configuration.

### Project Structure

```text
BooksLibrary/
├── BooksLibrary.Api/
├── BooksLibrary.Application/
├── BooksLibrary.Domain/
├── BooksLibrary.Infrastructure/
├── BooksLibrary.slnx
└── README.md
```

| Project | Responsibility | Main contents |
| --- | --- | --- |
| `BooksLibrary.Api` | Presentation layer | Controllers, `Program.cs`, `appsettings.json` |
| `BooksLibrary.Application` | Application layer | `AuthorDto`, `BookDto` |
| `BooksLibrary.Domain` | Domain layer | `Author`, `Book`, repository interfaces |
| `BooksLibrary.Infrastructure` | Infrastructure layer | `AuthorRepository`, `BookRepository`, FakeRestAPI models |

## Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

## Getting Started

Clone the repository and restore the dependencies:

```bash
git clone https://github.com/luismanuelcldev/books-library-api.git
cd books-library-api
dotnet restore BooksLibrary.slnx
```

Run the API:

```bash
dotnet run --project BooksLibrary.Api/BooksLibrary.Api.csproj
```

The API starts at `http://localhost:5001`.

Swagger UI is available at:

```text
http://localhost:5001/swagger
```

## API Endpoints

### Books

| Method | Endpoint | Description |
| --- | --- | --- |
| GET | `/api/books` | Get all books |
| GET | `/api/books/{id}` | Get a book by ID |
| POST | `/api/books` | Create a new book |
| PUT | `/api/books/{id}` | Update an existing book |
| DELETE | `/api/books/{id}` | Delete a book |

### Authors

| Method | Endpoint | Description |
| --- | --- | --- |
| GET | `/api/authors` | Get all authors |
| GET | `/api/authors/{id}` | Get an author by ID |
| POST | `/api/authors` | Create a new author |
| PUT | `/api/authors/{id}` | Update an existing author |
| DELETE | `/api/authors/{id}` | Delete an author |

## Data Source

This project does not use a database. All operations are delegated to FakeRestAPI at `https://fakerestapi.azurewebsites.net/` through the infrastructure repositories.

## Frontend

The frontend for this technical test is available in the [Books Library Client repository](https://github.com/luismanuelcldev/books-library-client).

## Notes

- CORS is configured to allow requests from a separate frontend application.
- Swagger is enabled in the Development environment.
- **FakeRestAPI limitations:** POST, PUT, and DELETE operations return successful responses, but changes are not persisted by FakeRestAPI. Newly created, updated, or deleted items may not appear in subsequent GET requests. This is the expected behavior of the third-party mock service.
