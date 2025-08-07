using JokeAPI.Models;
using Newtonsoft.Json;

namespace JokeAPI.Services
{
    public class JokeAPIService : IJokeAPIService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public JokeAPIService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Joke> GetRandomJokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetStringAsync("https://official-joke-api.appspot.com/random_joke");
            return JsonConvert.DeserializeObject<Joke>(response);
        }
    }
}
