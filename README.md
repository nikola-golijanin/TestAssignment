# TestAssignmentApi

## Prerequisites

Before you begin, ensure you have the following installed on your machine:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Optional] [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) with the ASP.NET and web development workload
- [PostgreSQL](https://www.postgresql.org/download/)

## Getting Started

### Clone the Repository
git clone https://github.com/your-repo/TestAssignmentApi.git cd TestAssignmentApi


### Configuration

1. **Database Configuration**: Update the `appsettings.json` file with your PostgreSQL connection string.

{ "ConnectionStrings": { "PostgresConnection": "Host=localhost;Database=your_db;Username=your_user;Password=your_password" } }

### Updating the Database

#### Using Visual Studio

1. Open the Package Manager Console from `Tools > NuGet Package Manager > Package Manager Console`.
2. Run the following commands to apply migrations and update the database:
`Update-Database`

#### Using .NET CLI

1. Open a terminal and navigate to the project directory.
2. Run the following commands to apply migrations and update the database:
`dotnet ef database update`

### Running the Application

#### Using Visual Studio

1. Open the solution file `TestAssignmentApi.sln` in Visual Studio.
2. Set `TestAssignmentApi` as the startup project.
3. Press `F5` to run the application.

#### Using .NET CLI

1. Open a terminal and navigate to the project directory.
2. Run the following command to start the application:

`dotnet run --project TestAssignmentApi`


## Swagger

The application uses Swagger for API documentation. Once the application is running, you can access the Swagger UI at `https://localhost:5001/swagger`.

## Postman Collection

A Postman collection named `TestAssignmentAPI.postman_collection` exists and can be imported into Postman for testing the API. This collection includes various requests to help you interact with and test the endpoints of the TestAssignmentApi.

To import the collection into Postman:
1. Open Postman.
2. Click on the `Import` button.
3. Select the `TestAssignmentAPI.postman_collection` file.
4. Click `Open` to import the collection.

You can now use the imported collection to test the API endpoints.

## Troubleshooting

If you encounter any issues, ensure that your PostgreSQL server is running and that the connection string in `appsettings.json` is correct.
