using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IHffA7.Models.dbModels;

namespace IHffA7.Models
{
    interface IFilmRepository
    {
        IEnumerable<Filmview> GetFilms();
        IEnumerable<Filmview> GetFilms(string dag);
        Activiteit GetLocatie();
        Filmview GetFilm(Filmview film);
        void AddFilm(Filmview film);
        void RemoveFilm(Filmview film);
    }
}
