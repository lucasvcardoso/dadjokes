using NUnit.Framework;
using IHazDadJokes.Infrastructure.HttpLib;
using System.Net;
using System.Threading.Tasks;
using System;

namespace IHazDadJokes.HttpLib.Tests
{
    [TestFixture]
    public class HttpClientWrapperTests
    {
        private const string CorrectTestUrl = "https://icanhazdadjoke.com/";
        private const string IncorrectTestUrl = "https://icanhazdadjoke.com/nonexistentendpoint";

        [Test]
        public async Task Response200WhenGetCorrectUrl()
        {
            var testee = new HttpClientWrapper();

            var response = await testee.Get(CorrectTestUrl);
            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Content);
        }

        [Test]
        public async Task Response404WhenGetIncorrectUrl()
        {
            var testee = new HttpClientWrapper();

            var response = await testee.Get(IncorrectTestUrl);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public void WhenCalledWithInvalidUrlThrowsInvalidOperationException()
        {
            var testee = new HttpClientWrapper();

            async Task act() => await testee.Get("Not an URL");

            Assert.That(act, Throws.InvalidOperationException);
        }
    }
}
