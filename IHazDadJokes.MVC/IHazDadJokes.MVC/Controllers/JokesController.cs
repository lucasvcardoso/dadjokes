using IHazDadJokes.API.Lib;
using IHazDadJokes.Infrastructure.HttpLib;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Mvc;
using DadJoke = IHazDadJokes.API.Lib.DadJoke;

namespace IHazDadJokes.MVC.Controllers
{
    public class JokesController : Controller
    {
        private readonly IDadJokesService _dadJokesService;
        private readonly IDadJokesServiceConfiguration _serviceConfig;

        public JokesController()
        {
            _serviceConfig = ConfigurationManager.GetSection("jokes") as DadJokesServiceConfiguration;
            _dadJokesService = new DadJokesService(new HttpClientWrapper(), _serviceConfig); 
        }

        [HttpGet]
        public async Task<ActionResult> RandomDadJoke()
        {
            return View(await GetRandomJoke());
        }        

        [HttpGet]
        public async Task<ActionResult> PartialRandomDadJoke()
        {
            return PartialView(await GetRandomJoke());
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

            var viewModel = await _dadJokesService.GetDadJokesBySearchTerm(searchTerm);

            return PartialView(viewModel);
        }

        private async Task<DadJoke> GetRandomJoke()
        {
            return await _dadJokesService.GetRandomDadJoke();
        }
    }
}