﻿using IHazDadJokes.Infrastructure.HttpLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace IHazDadJokes.API.Lib
{
    public class DadJokesService : IDadJokesService
    {
        private readonly IHttpClientWrapper _httpClient;

        public DadJokesService(IHttpClientWrapper httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DadJokesViewModel> GetDadJokesBySearchTerm(string url, string searchTerm, int limit)
        {
            var searchUrl = $"{url}/search?term={searchTerm}&limit={limit}";
            var dadJokeResponseMessage = await _httpClient.Get(searchUrl);
            var dadJokes = await ProcessDadJokesList(dadJokeResponseMessage, searchTerm);
            return dadJokes;
        }

        public async Task<DadJoke> GetRandomDadJoke(string url)
        {            
            var dadJokeResponseMessage = await _httpClient.Get(url);
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
                SearchTerm = searchTerm,
                LongDadJokes = dadJokesList
            };

            return dadJokesViewModel;
        }       

        private static async Task<string> ReadDadJokesContent(HttpResponseMessage dadJokeResponseMessage)
        {
            return await dadJokeResponseMessage.Content.ReadAsStringAsync();
        }
    }
}
