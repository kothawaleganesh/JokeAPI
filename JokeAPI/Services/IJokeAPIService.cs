using JokeAPI.Models;

namespace JokeAPI.Services
{
    public interface IJokeAPIService
    {
        Task<Joke> GetRandomJokeAsync();
    }
}
