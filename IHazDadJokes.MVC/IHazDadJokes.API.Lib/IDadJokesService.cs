﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHazDadJokes.API.Lib
{
    public interface IDadJokesService
    {
        Task<DadJoke> GetRandomDadJoke();
        Task<DadJokesViewModel> GetDadJokesBySearchTerm(string searchTerm);
    }
}
