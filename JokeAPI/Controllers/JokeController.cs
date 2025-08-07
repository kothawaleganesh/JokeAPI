using JokeAPI.Models;
using JokeAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace JokeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JokeController : ControllerBase
    {
        private readonly JokeConsumer _jokeConsumer;

        public JokeController(JokeConsumer jokeConsumer)
        {
            _jokeConsumer = jokeConsumer;
        }


        [HttpGet(Name = "GetJoke")]
        public async Task<Joke> GetAsync()
        {
            return await _jokeConsumer.GetJokeAsync();
        }
    }
}
