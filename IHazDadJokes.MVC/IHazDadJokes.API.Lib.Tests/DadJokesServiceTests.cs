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

        private string _json;
        private string _jsonList;// = "{\"current_page\":1,\"limit\":2,\"next_page\":1,\"previous_page\":1,\"results\":[{\"id\":\"Id\",\"joke\":\"Joke\"},{\"id\":\"Id2\",\"joke\":\"Joke2\"}],\"search_term\":\"term\",\"status\":200,\"total_jokes\":3,\"total_pages\":1}";

        private Mock<IHttpClientWrapper> _httpClientMock;

        private HttpResponseMessage _responseSingleJoke = new HttpResponseMessage();
        private HttpResponseMessage _responseListJokes = new HttpResponseMessage();

        private DadJokesServiceConfiguration _config = new DadJokesServiceConfiguration
        {
            Limit = 30,
            Url = "https://icanhazdadjokes.com/"
        };

        [SetUp]
        public void SetUp()
        {
            _json = TestHelper.GetResourceFileContent($"{GetType().Namespace}.TestData", "SingleJoke.json");

            _jsonList = TestHelper.GetResourceFileContent($"{GetType().Namespace}.TestData", "JokesList.json");

            _httpClientMock = new Mock<IHttpClientWrapper>();

            _testee = new DadJokesService(_httpClientMock.Object, _config);
            
            _responseSingleJoke.Content = new StringContent(_json);

            _responseListJokes.Content = new StringContent(_jsonList);

        }

        [Test]
        public async Task GetsRandomDadJokeCorrectly()
        {
            _httpClientMock.Setup(_ => _.Get(It.IsAny<string>())).ReturnsAsync(_responseSingleJoke);
            var dadJoke = await _testee.GetRandomDadJoke();

            Assert.IsNotNull(dadJoke.Id);
            Assert.IsNotNull(dadJoke.Joke);
            Assert.AreEqual("Id", dadJoke.Id);
            Assert.AreEqual("Joke", dadJoke.Joke);

        }

        [Test]
        public async Task GetsDadJokesListCorrectly()
        {
            _httpClientMock.Setup(_ => _.Get(It.IsAny<string>())).ReturnsAsync(_responseListJokes);
            var dadJokes = await _testee.GetDadJokesBySearchTerm("term");

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
