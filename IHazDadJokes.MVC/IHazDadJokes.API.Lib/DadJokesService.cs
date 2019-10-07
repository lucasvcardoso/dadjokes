using IHazDadJokes.Infrastructure.HttpLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace IHazDadJokes.API.Lib
{
    public class DadJokesService : IDadJokesService
    {
        private readonly IHttpClientWrapper _httpClient;
        private readonly IDadJokesServiceConfiguration _config;
        private readonly string _serviceUrl;
        private readonly int _limit;

        public DadJokesService(IHttpClientWrapper httpClient, IDadJokesServiceConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
            _serviceUrl = _config.Url;
            _limit = _config.Limit;
        }

        public async Task<DadJokesViewModel> GetDadJokesBySearchTerm(string searchTerm)
        {
            var searchUrl = $"{_serviceUrl}/search?term={searchTerm}&limit={_limit}";
            var dadJokeResponseMessage = await _httpClient.Get(searchUrl);
            var dadJokes = await ProcessDadJokesList(dadJokeResponseMessage, searchTerm);
            return dadJokes;
        }

        public async Task<DadJoke> GetRandomDadJoke()
        {            
            var dadJokeResponseMessage = await _httpClient.Get(_serviceUrl);
            var dadJoke = await ProcessDadJoke(dadJokeResponseMessage);
            return dadJoke;
        }

        private async Task<DadJoke> ProcessDadJoke(HttpResponseMessage dadJokeResponseMessage)
        {
            string content = await ReadDadJokesContent(dadJokeResponseMessage);
            var dadJoke = JsonConvert.DeserializeObject<DadJoke>(content);
            return dadJoke;
        }        

        private async Task<DadJokesViewModel> ProcessDadJokesList(HttpResponseMessage dadJokeResponseMessage, string searchTerm)
        {
            var content = await ReadDadJokesContent(dadJokeResponseMessage);
            var dadJokesJObject = JObject.Parse(content);
            var dadJokesToken = dadJokesJObject["results"].ToString();
            var dadJokesList = JsonConvert.DeserializeObject<List<DadJoke>>(dadJokesToken);

            var dadJokesViewModel = new DadJokesViewModel {
                SearchTerm = string.IsNullOrEmpty(searchTerm) ? "<<Any word>>" : searchTerm,
                ShortDadJokes = GetDadJokesWithLength(JokeLength.Short, dadJokesList),
                MediumDadJokes = GetDadJokesWithLength(JokeLength.Medium, dadJokesList),
                LongDadJokes = GetDadJokesWithLength(JokeLength.Long, dadJokesList)
            };

            return dadJokesViewModel;
        }       

        private static async Task<string> ReadDadJokesContent(HttpResponseMessage dadJokeResponseMessage)
        {
            return await dadJokeResponseMessage.Content.ReadAsStringAsync();
        }

        private List<DadJoke> GetDadJokesWithLength(JokeLength length, List<DadJoke> list)
        {
            Func<DadJoke, bool> selector = null;

            switch (length)
            {
                case JokeLength.Short:
                    selector = j => CountJokeWords(j) < 10;
                    break;
                case JokeLength.Medium:
                    selector = j => CountJokeWords(j) >= 10 && CountJokeWords(j) < 20; 
                    break;
                case JokeLength.Long:
                    selector = j => CountJokeWords(j) >= 20;
                    break;
            }

            return list.Where(selector).ToList();
        }

        private int CountJokeWords(DadJoke joke)
        {
            var text = joke.Joke;
            var punctuation = text.Where(Char.IsPunctuation).Distinct().ToArray();
            var wordCount = text.Split().Select(j => j.Trim(punctuation)).Count();
            return wordCount;
        }

        private enum JokeLength
        {
            Short,
            Medium,
            Long
        }
    }
}
