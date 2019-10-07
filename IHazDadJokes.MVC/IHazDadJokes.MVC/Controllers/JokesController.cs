using IHazDadJokes.API.Lib;
using IHazDadJokes.Infrastructure.HttpLib;
using System.Threading.Tasks;
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

        [HttpGet]
        public ActionResult SearchDadJoke()
        {
            ViewBag.Message = "Your Dad Jokes will be displayed here!";

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> PartialSearchDadJokes()
        {
            ViewBag.Message = "Here are the dad jokes with the word you searched!";
            ViewBag.IsResult = true;
            var searchTerm = Request["SearchTerm"];

            var viewModel = await _dadJokesService.GetDadJokesBySearchTerm(ServiceUrl, searchTerm, 30);

            return PartialView(viewModel);
        }

        private async Task<DadJoke> GetRandomJoke(string url)
        {
            return await _dadJokesService.GetRandomDadJoke(url);
        }
    }
}