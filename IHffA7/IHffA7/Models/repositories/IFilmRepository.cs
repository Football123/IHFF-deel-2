using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHffA7.Models
{
    interface IFilmRepository
    {
        IEnumerable<Film> GetFilms();
        IEnumerable<Film> GetFilms(string dag);
        Film GetFilm(Film film);
        void AddFilm(Film film);
        void RemoveFilm(Film film);
    }
}
