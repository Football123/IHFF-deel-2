﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHffA7.Models.repositories
{
    interface IHomeRepository
    {
        IEnumerable<FilmsTodayModel> GetTodaysMovies();
        int ConvertToFilmId(FilmsTodayModel ftm);
    }
}
