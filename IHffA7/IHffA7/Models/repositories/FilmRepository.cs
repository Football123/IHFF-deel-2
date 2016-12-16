using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IHffA7.Models.dbModels;

namespace IHffA7.Models.repositories
{
    class FilmRepository
    {
        private IhffA7Context ctx = new IhffA7Context();
        public Films GetFilm(int filmId)
        {
            return ctx.Film.SingleOrDefault(c => (c.Id == filmId));
            ///hoid
        }
    }
}
