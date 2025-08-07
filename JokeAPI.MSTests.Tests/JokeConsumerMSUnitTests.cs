using JokeAPI.Models;
using JokeAPI.Services;
using Moq;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace JokeAPI.Tests
{
    [TestClass]
    public class JokeConsumerMSUnitTests
    {
        [TestMethod]
        public async Task GetRandomJokeAsync_Returns_Joke_From_API_FromMSTest()
        {
            // Arrange
            var expectedJoke = new Joke { Setup = "Did you hear that David lost his ID in prague?", Punchline = "Now we just have to call him Dav." };
            var mockAPI = new Mock<IJokeAPIService>();
            mockAPI.Setup(x => x.GetRandomJokeAsync()).ReturnsAsync(expectedJoke);
            var consumer = new JokeConsumer(mockAPI.Object);
            // act
            var result = await consumer.GetJokeAsync();

            // Assert
            Assert.AreEqual(expectedJoke.Setup, result.Setup);
            Assert.AreEqual(expectedJoke.Punchline, result.Punchline);
            mockAPI.Verify(x => x.GetRandomJokeAsync(), Times.Exactly(1));
        }
    }
}
