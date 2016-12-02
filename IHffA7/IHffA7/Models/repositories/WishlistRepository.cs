using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IHffA7.Models.dbModels;

namespace IHffA7.Models.repositories
{
    class WishlistRepository
    {
        IhffA7Context ctx = new IhffA7Context();
        public IEnumerable<WishlistItems>GetWishlistItems(int wishlistId)
        {
            //return ctx.WishlistItem.Where(c => (c.WishlistId == wishlistId)).ToList();
            return ctx.WishlistItem.ToList();
        }

        public IEnumerable<Locations> GetLocations()
        {
            return ctx.Location.ToList();
        }
        public Activities GetActiviteit(int wishlistItem)
        {
            return ctx.Activity.SingleOrDefault(c => (c.Id == wishlistItem));
        }

        public FilmScreenings GetFilmvoorstelling(int activiteitId)
        {
            return ctx.FilmScreening.SingleOrDefault(c => (c.ActivityId == activiteitId));
        }
        public Films GetFilm(int filmId)
        {
            return ctx.Film.SingleOrDefault(c => (c.Id == filmId));
        }
    }
}
