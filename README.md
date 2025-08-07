# JokeAPI – Project README

## Overview

JokeAPI is a clean, testable **ASP.NET Core Web API** (.NET 9) that serves random jokes from an external public API. It demonstrates best practices such as:

- Separation of concerns (Controller/Service/Model)
- Dependency injection
- Using HttpClientFactory for external calls
- JSON model mapping
- Mocking and unit tests with xUnit, MSTest, and NUnit

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- IDE like Visual Studio 2022+ or VS Code

## Project Structure
JokeAPI
│
├── Controllers/
│     └── JokeController.cs
│
├── Models/
│     └── Joke.cs
│
├── Services/
│     ├── IJokeAPIService.cs
│     ├── JokeAPIService.cs
│     └── JokeConsumer.cs
│
├── Program.cs
│
├── JokeAPI.Tests/            # xUnit tests
│     └── JokeConsumerXUnitTests.cs
│
├── JokeAPI.MSTests.Tests/    # MSTest tests
│     └── JokeConsumerMSUnitTests.cs
│
├── JokeAPI.Nunit.Tests/      # NUnit tests
│     └── JokeConsumerNUnitTests.cs
│
└── ... other standard files (csproj, launchSettings, etc)
## Components in Detail

### 1. **Model: Joke**

**File:** `Models/Joke.cs`

- Represents the structure of a joke returned by the external API.
- Properties:
  - `Setup` (string): The question or initial part of the joke.
  - `Punchline` (string): The answer or funny part of the joke.

### 2. **Service Interface: IJokeAPIService**

**File:** `Services/IJokeAPIService.cs`

- Declares an abstraction for requesting a random joke.
- Enables dependency inversion and mocking during unit testing.

### 3. **Service Implementation: JokeAPIService**

**File:** `Services/JokeAPIService.cs`

- Implements `IJokeAPIService`.
- Uses `IHttpClientFactory` for robust, reusable HTTP communication.
- Fetches a joke as JSON and deserializes it into a `Joke` object using `System.Text.Json`.

### 4. **Consumer Service: JokeConsumer**

**File:** `Services/JokeConsumer.cs`

- Depends on `IJokeAPIService`.
- Provides business logic and acts as an adapter between API controllers and the lower-level service.

### 5. **Controller: JokeController**

**File:** `Controllers/JokeController.cs`

- ASP.NET Core Web API controller.
- Route: `/api/joke`
- Uses constructor injection for `JokeConsumer`.
- Responds with a random joke.

### 6. **Startup: Program.cs**

- Registers required services (`Controllers`, `IHttpClientFactory`, service implementations).
- Maps the controller routes.
- Uses the minimal hosting model for .NET 9.

## External Dependencies Used

- **HttpClientFactory**  
  For efficient and scalable HTTP communications.
- **System.Text.Json**  
  For fast and modern JSON serialization/deserialization.
- **Dependency Injection (built-in)**  
  Ensures loose coupling and easy unit testing.
- **Moq**  
  For mocking dependencies in tests.

## Testing

### Test Projects

- `JokeAPI.Tests` (xUnit)
- `JokeAPI.MSTests.Tests` (MSTest)
- `JokeAPI.Nunit.Tests` (NUnit)

Each contains equivalent tests for the `JokeConsumer` class. All tests use Moq to mock the `IJokeAPIService` dependency.

#### Example: xUnit Test
[Fact]
public async Task GetRandomJokeAsync_Returns_Joke_From_API_FromXunit()
{
    var expectedJoke = new Joke { Setup = "Did you hear that David lost his ID in prague?", Punchline = "Now we just have to call him Dav." };
    var mockAPI = new Mock<IJokeAPIService>();
    mockAPI.Setup(x => x.GetRandomJokeAsync()).ReturnsAsync(expectedJoke);
    var consumer = new JokeConsumer(mockAPI.Object);

    var result = await consumer.GetJokeAsync();

    Assert.Equal(expectedJoke.Setup, result.Setup);
    Assert.Equal(expectedJoke.Punchline, result.Punchline);
    mockAPI.Verify(x => x.GetRandomJokeAsync(), Times.Exactly(1));
}
#### Run Tests

- In Visual Studio: Use Test Explorer.
- .NET CLI:dotnet test
## API Usage Example

**GET** `/api/joke`

Response body:{
  "setup": "Why did the chicken cross the road?",
  "punchline": "To get to the other side!"
}
## Notes & Best Practices

- **Separation of Concerns:** Controller, Consumer Service, and API Service all play distinct roles.
- **Open/Closed for Testing:** All logic is tested in isolation, and external dependencies are mockable.
- **Strong Typing:** Mapping API payloads to C# classes keeps your code maintainable.
- **Extensible:** You can swap `JokeAPIService` with any other source (database, different API) without changing upper layers.
- **HttpClient Best Practice:** Using `IHttpClientFactory` is recommended by Microsoft for production scenarios.
- **Moq:** Common de facto mocking library for .NET testing.

## To Run Locally

1. **Restore NuGet packages**```
dotnet restore
```2. **Run the API**```
dotnet run --project JokeAPI
```3. **Test the endpoint**
    - Use a tool like `curl`, Postman, or visit `https://localhost:5001/api/joke` in your browser.

4. **Run Tests**```
dotnet test
```
## Extending This Project

- Add error handling (try-catch, API availability checks)
- Add more endpoints or support for joke categories
- Implement logging
- Containerize via Docker

## Summary Table

| Layer         | File(s)                                         | Responsibility                        |
|---------------|-------------------------------------------------|---------------------------------------|
| API           | JokeController.cs                               | Expose API endpoint (/api/joke)       |
| Consumer      | JokeConsumer.cs                                 | Business/aggregation logic            |
| Service       | IJokeAPIService.cs, JokeAPIService.cs           | Encapsulate external API details      |
| Model         | Joke.cs                                         | Data structures                       |
| Startup       | Program.cs                                      | Configure services, DI, http pipeline |
| Tests         | JokeConsumerXUnitTests.cs, JokeConsumerMSUnitTests.cs, JokeConsumerNUnitTests.cs | Fast, isolated unit tests             |

## Questions?

Open an issue or contact the maintainer!

**Happy coding!**