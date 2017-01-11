using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace IHffA7.Models.repositories
{
    public class ActivitiesRepository
    {
        WhatsUp1617S_martinstinsGenerated ctx = new WhatsUp1617S_martinstinsGenerated(); //test offline db

        public IQueryable<Activities> GetActivities()
        {
            return ctx.Activities
                .Include(s => s.Filmscreenings)
                .Include(f => f.Filmscreenings.Select(ff => ff.Films));
        }
        public void SaveFilmScreening(Filmscreenings filmscreening)
        {
            //test
            ctx.Activities.Add(filmscreening.Activities);
            ctx.Filmscreenings.Add(filmscreening);
            ctx.SaveChanges();
        }
        public void SaveFilm(Films film)
        {
            //test
            ctx.Films.Add(film);
            ctx.SaveChanges();
        }

        public void SaveRestaurant(Restaurants restaurant)
        {
            ctx.Restaurants.Add(restaurant);
            ctx.SaveChanges();
        }

        public void SaveSpecial(Specials special)
        {
            ctx.Specials.Add(special);
            ctx.SaveChanges();
        }

        public void ModifyActivity(Filmscreenings film)
        {
            ctx.Entry(film.Activities).State = EntityState.Modified;
           // ctx.Entry(film).State = EntityState.Modified;
            ctx.SaveChanges();
        }
        //not yet used
        public void ModifyActivity(Specialscreenings Special)
        {
            ctx.Entry(Special.Activities).State = EntityState.Modified;
            ctx.Entry(Special).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public Filmscreenings GetFilmscreening(int? activityId)
        {
            return ctx.Filmscreenings.Single(a => (a.Activities.id == activityId));
        }

        public void DeleteFilmScreening(Filmscreenings film)
        {
            ctx.Activities.Remove(film.Activities);
            ctx.Filmscreenings.Remove(film);
            ctx.SaveChanges();
        }
    }
}
