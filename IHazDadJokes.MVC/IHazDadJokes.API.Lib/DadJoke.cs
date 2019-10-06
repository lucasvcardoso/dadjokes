using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHazDadJokes.API.Lib
{
    public class DadJoke
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Joke { get; set; }
    }
}
