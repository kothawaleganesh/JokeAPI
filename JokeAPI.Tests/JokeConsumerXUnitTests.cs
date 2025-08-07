using JokeAPI.Models;
using JokeAPI.Services;
using Moq;
using System.Threading.Tasks;

namespace JokeAPI.Tests
{
    public class JokeConsumerXUnitTests
    {
        [Fact]
        public async Task GetRandomJokeAsync_Returns_Joke_From_API_FromXunit()
        {
            // Arrange
            var expectedJoke = new Joke { Setup = "Did you hear that David lost his ID in prague?", Punchline = "Now we just have to call him Dav." };
            var mockAPI = new Mock<IJokeAPIService>();
            mockAPI.Setup(x => x.GetRandomJokeAsync()).ReturnsAsync(expectedJoke);
            var consumer = new JokeConsumer(mockAPI.Object);
            // act
            var result = await consumer.GetJokeAsync();

            // Assert
            Assert.Equal(expectedJoke.Setup,result.Setup);
            Assert.Equal(expectedJoke.Punchline,result.Punchline);
            mockAPI.Verify(x => x.GetRandomJokeAsync(), Times.Exactly(1));

        }
    }
}
