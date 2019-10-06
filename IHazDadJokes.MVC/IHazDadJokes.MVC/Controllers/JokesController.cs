using IHazDadJokes.API.Lib;
using IHazDadJokes.Infrastructure.HttpLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DadJoke = IHazDadJokes.API.Lib.DadJoke;

namespace IHazDadJokes.MVC.Controllers
{
    public class JokesController : Controller
    {
        private readonly IDadJokesService _dadJokesService;
        private const string ServiceUrl = "https://icanhazdadjoke.com/";

        public JokesController()
        {
            _dadJokesService = new DadJokesService(new HttpClientWrapper());
        }

        [HttpGet]
        public async Task<ActionResult> RandomDadJoke()
        {
            return View(await GetRandomJoke(ServiceUrl));
        }        

        [HttpGet]
        public async Task<ActionResult> PartialRandomDadJoke()
        {
            return PartialView(await GetRandomJoke(ServiceUrl));
        }

        private async Task<DadJoke> GetRandomJoke(string url)
        {
            return await _dadJokesService.GetRandomDadJoke(url);
        }

        //[HttpGet]
        //public ActionResult 
    }
}