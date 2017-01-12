using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHffA7.Models.repositories
{
    class FilmRepository
    {
        private WhatsUp1617S_martinstinsGenerated ctx = new WhatsUp1617S_martinstinsGenerated();
        public Films GetFilm(int filmId)
        {
            var i = 
                ctx.Films.SingleOrDefault (c => (c.id == filmId));
            return i;
            
        }
        public IEnumerable<Films> GetFilms()
        {
            var i =
                ctx.Films;
            return i;
        }
        public List<Films> GetFilmMonday()
        {
            List<Films> films = new List<Films>();
            var test = ctx.Films;
            foreach(var film in test)
            {
                var datetime =film.Filmscreenings.Select(x=>x.Activities.startTime);
                foreach(var datum in datetime)
                if (datum.DayOfYear == DateTime.Today.DayOfYear)
                {
                    films.Add(film);
                }
            }
            return films;          
        }
        public List<Films> GetFilmTuesday()
        {
            List<Films> films = new List<Films>();
            var test = ctx.Films;
            foreach (var film in test)
            {
                var datetime = film.Filmscreenings.Select(x => x.Activities.startTime);
                foreach (var datum in datetime)
                    if (datum.DayOfYear == DateTime.Today.DayOfYear)
                    {
                        films.Add(film);
                    }
            }
            return films;
        }
        public List<Films> GetFilmWednesday()
        {
            List<Films> films = new List<Films>();
            var test = ctx.Films;
            foreach (var film in test)
            {
                var datetime = film.Filmscreenings.Select(x => x.Activities.startTime);
                foreach (var datum in datetime)
                    if (datum.DayOfYear == DateTime.Today.DayOfYear)
                    {
                        films.Add(film);
                    }
            }
            return films;
        }
        public List<Films> GetFilmThursday()
        {
            List<Films> films = new List<Films>();
            var test = ctx.Films;
            foreach (var film in test)
            {
                var datetime = film.Filmscreenings.Select(x => x.Activities.startTime);
                foreach (var datum in datetime)
                    if (datum.DayOfYear == DateTime.Today.DayOfYear)
                    {
                        films.Add(film);
                    }
            }
            return films;
        }
        public List<Films> GetFilmFriday()
        {
            List<Films> films = new List<Films>();
            var test = ctx.Films;
            foreach (var film in test)
            {
                var datetime = film.Filmscreenings.Select(x => x.Activities.startTime);
                foreach (var datum in datetime)
                    if (datum.DayOfYear == DateTime.Today.DayOfYear)
                    {
                        films.Add(film);
                    }
            }
            return films;
        }
    }
}
