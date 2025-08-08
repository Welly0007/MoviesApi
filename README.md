# MovieApp

MovieApp is a RESTful Web API built with ASP.NET Core (.NET 8) for managing movies and genres. It demonstrates modern API development practices, including Entity Framework Core integration, async/await usage, and OpenAPI (Swagger) documentation.

## Features

- CRUD operations for movies and genres
- Updated MoviesService with improved logic and best practices for movie operations
- Entity Framework Core with SQL Server for data persistence
- Asynchronous controller actions for scalability
- Lazy loading is used where necessary for related data
- OpenAPI/Swagger UI for interactive API documentation and testing

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or change the connection string for your database)

### Setup

1. **Clone the repository:** git clone <https://github.com/Welly0007/MoviesApi>
cd MovieApp2. **Configure the database connection:**
   - Update the `DefaultConnection` string in `appsettings.json` to point to your SQL Server instance.

3. **Apply database migrations:** dotnet ef database update4. **Run the application:** dotnet run5. **Access Swagger UI:**
   - Navigate to `https://localhost:<port>/swagger` in your browser to explore and test the API.

## Project Structure

- `Controllers/` – API controllers for movies and genres
- `Services/` – Business logic for movies (including updated MoviesService)
- `Models/` – Entity and DTO classes
- `Data/` – Entity Framework Core DbContext and migrations
- `Program.cs` – Application entry point and configuration

## API Documentation

Interactive API documentation is available via Swagger UI. All endpoints, request/response schemas, and example payloads are documented automatically.

## Contributing																					

Contributions are welcome! Please open issues or submit pull requests for improvements or bug fixes.

## License

This project is licensed under the MIT License.
