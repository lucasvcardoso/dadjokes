using IHazDadJokes.Infrastructure.HttpLib;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IHazDadJokes.API.Lib.Tests
{
    [TestFixture]
    public class DadJokesServiceTests
    {
        private DadJokesService _testee;

        private const string CorrectTestUrl = "https://icanhazdadjoke.com/";
        private const string Json = "{\"id\":\"Id\",\"joke\":\"Joke\"}";
        private const string JsonList = "{\"current_page\":1,\"limit\":2,\"next_page\":1,\"previous_page\":1,\"results\":[{\"id\":\"Id\",\"joke\":\"Joke\"},{\"id\":\"Id2\",\"joke\":\"Joke2\"}],\"search_term\":\"term\",\"status\":200,\"total_jokes\":3,\"total_pages\":1}";

        private Mock<IHttpClientWrapper> _httpClientMock;

        private HttpResponseMessage _responseSingleJoke = new HttpResponseMessage();
        private HttpResponseMessage _responseListJokes = new HttpResponseMessage();

        [SetUp]
        public void SetUp()
        {
            _httpClientMock = new Mock<IHttpClientWrapper>();

            _testee = new DadJokesService(_httpClientMock.Object);
            
            _responseSingleJoke.Content = new StringContent(Json);

            _responseListJokes.Content = new StringContent(JsonList);
        }

        [Test]
        public async Task GetsRandomDadJokeCorrectly()
        {
            _httpClientMock.Setup(_ => _.Get(It.IsAny<string>())).ReturnsAsync(_responseSingleJoke);
            var dadJoke = await _testee.GetRandomDadJoke(CorrectTestUrl);

            Assert.IsNotNull(dadJoke.Id);
            Assert.IsNotNull(dadJoke.Joke);
            Assert.AreEqual("Id", dadJoke.Id);
            Assert.AreEqual("Joke", dadJoke.Joke);

        }

        [Test]
        public async Task GetsDadJokesListCorrectly()
        {
            _httpClientMock.Setup(_ => _.Get(It.IsAny<string>())).ReturnsAsync(_responseListJokes);
            var dadJokes = await _testee.GetDadJokesBySearchTerm(CorrectTestUrl, "term", 30);

            Assert.IsNotNull(dadJokes);
            Assert.AreEqual(2, dadJokes.ShortDadJokes.Count());
            Assert.AreEqual("Id", dadJokes.ShortDadJokes[0].Id);
            Assert.AreEqual("Joke", dadJokes.ShortDadJokes[0].Joke);
            Assert.AreEqual("Id2", dadJokes.ShortDadJokes[1].Id);
            Assert.AreEqual("Joke2", dadJokes.ShortDadJokes[1].Joke);
            Assert.AreEqual(0, dadJokes.MediumDadJokes.Count());
            Assert.AreEqual(0, dadJokes.LongDadJokes.Count());
        }
    }
}
