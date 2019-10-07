using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHazDadJokes.API.Lib
{
    public class DadJokesViewModel
    {

        [Required]
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
