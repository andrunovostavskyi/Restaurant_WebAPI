
# Clean Architecture Web API with ASP.NET Core 8

## Description
This project is a backend solution for a web application that performs CRUD operations, handles file uploads, and ensures secure user authentication and authorization. It leverages **Clean Architecture**, **CQRS**, and **Postgres** for scalability and maintainability.

Key features include:
- RESTful API development with ASP.NET Core 8.
- Authentication and authorization for API users.
- Custom middleware for request/response handling.
- File handling: upload to the server and return files from the API.
- Centralized logging with **Serilog**.
- Automated unit and integration tests for quality assurance.

---

## Features
- **Database Integration**: Postgres database built using C# classes with Entity Framework Core.
- **Clean Architecture**: Maintainable and flexible codebase following CQRS and MediatR patterns.
- **Security**: Secure authentication and authorization with roles and custom claims.
- **File Management**: Upload and download files directly via API endpoints.
- **Error Handling**: Centralized exception handling with meaningful feedback for clients.
- **Logging**: Application and error logging using **Serilog** with outputs to text files.
- **Testing**: Automated unit and integration tests to ensure functionality and prevent regressions.

---

## Installation and Setup
1. Clone the repository.
2.  Navigate to the project directory.
3.  Install dependencies and restore the project.
4.  Add and fill the `appsettings.json` file with your Postgres connection string and other configurations.
5.  Apply migrations and seed the database.
6.  Run the application.
    

----------

## API Endpoints

### Authentication

-   `POST /api/auth/login` - User login.
-   `POST /api/auth/register` - User registration.

### CRUD Operations

-   `GET /api/resources` - Get all resources.
-   `GET /api/resources/{id}` - Get resource by ID.
-   `POST /api/resources` - Create a new resource.
-   `PUT /api/resources/{id}` - Update an existing resource.
-   `DELETE /api/resources/{id}` - Delete a resource.

----------

## Project Structure

The project follows Clean Architecture principles:

-   **Presentation Layer**: API controllers.
-   **Application Layer**: Business logic, DTOs, and CQRS commands/queries.
-   **Infrastructure Layer**: Database context, repositories, and file handling.
-   **Domain Layer**: Core entities and business rules.

----------

## Logging

Logging is implemented using **Serilog**, with logs written to text files for:

-   Application events.
-   Error details.

----------

## Tests

Automated tests are included for:

-   **Unit Tests**: Validate business logic.
-   **Integration Tests**: Ensure correct behavior of API endpoints and database interactions.

----------

## Future Enhancements
-   Deploy the application to the Azure cloud.
-   Add caching for improved performance.
-   Implement API rate limiting for security.
-   Explore GraphQL for more flexible queries.

----------

## License

This is a learning project and does not include a license. Contributions are welcome to improve the codebase.

----------

## Contact
If you have any questions or suggestions, feel free to reach out:

- **GitHub**: [andrunovostavskyi](https://github.com/andrunovostavskyi)
- **LinkedIn**: https://www.linkedin.com/in/andriy-novostavskyi-073879325/
- **Email**: novostavskuy@gmail.com
