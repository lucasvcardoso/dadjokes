using IHazDadJokes.API.Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IHazDadJokes.MVC.Models
{
    public class DadJokesViewModel
    {

        [Required]
        [Display(Name = "Search Joke with:")]
        public string SearchTerm { get; set; }

        /// <summary>
        /// Dad jokes < 10 words
        /// </summary>
        public List<DadJoke> ShortDadJokes { get; set; }

        /// <summary>
        /// Dad jokes > 10 && < 20 words
        /// </summary>
        public List<DadJoke> MediumDadJokes { get; set; }

        /// <summary>
        /// Dad jokes with >=20 words
        /// </summary>
        public List<DadJoke> LongDadJokes { get; set; }
    }
}