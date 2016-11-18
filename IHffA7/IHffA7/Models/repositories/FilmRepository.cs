using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IHffA7.Models;
using IHffA7.Models.dbModels;

namespace IHffA7.Models

{
    class FilmRepository : IFilmRepository
    {
        IhffA7Context ctx = new IhffA7Context();
        public void AddFilm(Filmview film)
        {
            throw new NotImplementedException();

        }

        public Filmview GetFilm(Filmview film)
        {
            throw new NotImplementedException();
        }

        public Activiteit GetLocatie()
        {
            //return ctx.Locatie.SingleOrDefault();
            return ctx.Activiteit.SingleOrDefault(d => (d.Id == 1));
        }

        public IEnumerable<Filmview> GetFilms()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Filmview> GetFilms(string dag)
        {
            throw new NotImplementedException();
        }

        public void RemoveFilm(Filmview film)
        {
            throw new NotImplementedException();
        }
    }
}
