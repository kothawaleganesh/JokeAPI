using JokeAPI.Models;
using JokeAPI.Services;
using Moq;

namespace JokeAPI.Nunit.Tests
{
    public class JokeConsumerNUnitTests
    {
        [Test]
        public async Task GetRandomJokeAsync_Returns_Joke_From_API_FromUnitTest()
        {
            // Arrange
            var expectedJoke = new Joke { Setup = "Did you hear that David lost his ID in prague?", Punchline = "Now we just have to call him Dav." };
            var mockAPI = new Mock<IJokeAPIService>();
            mockAPI.Setup(x => x.GetRandomJokeAsync()).ReturnsAsync(expectedJoke);
            var consumer = new JokeConsumer(mockAPI.Object);
            // Act
            var result = await consumer.GetJokeAsync();
            // Assert
            Assert.That(result.Setup, Is.EqualTo(expectedJoke.Setup)); // Updated to use Assert.That with Is.EqualTo
            Assert.That(result.Punchline, Is.EqualTo(expectedJoke.Punchline)); // Updated to use Assert.That with Is.EqualTo
            mockAPI.Verify(x => x.GetRandomJokeAsync(), Times.Exactly(1));
        }
    }
}
