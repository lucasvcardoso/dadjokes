using IHazDadJokes.API.Lib;
using IHazDadJokes.Infrastructure.HttpLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IHazDadJokes.MVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
           return View();
        }        
    }
}