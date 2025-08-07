using JokeAPI.Models;

namespace JokeAPI.Services
{
    public class JokeConsumer
    {
        private readonly IJokeAPIService _jokeAPIService;
        public JokeConsumer(IJokeAPIService jokeAPIService)
        {
            _jokeAPIService = jokeAPIService;
        }
        public async Task<Joke>GetJokeAsync()
        {
            return await _jokeAPIService.GetRandomJokeAsync();
        }
    }
}
