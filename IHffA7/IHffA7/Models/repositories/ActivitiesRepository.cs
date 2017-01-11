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
            ctx.Filmscreenings.Add(filmscreening);
            ctx.SaveChanges();
        }

        public void SaveSpecialsScreening(Specialscreenings specailscreening)
        {
            //activities toevoegen hoeft niet omdat het er al in zit (specailscreening.Activities)
            ctx.Specialscreenings.Add(specailscreening);
            ctx.SaveChanges();
        }
        public void SaveFilm(Films film)
        {
            ctx.Films.Add(film);
            ctx.SaveChanges();
        }

        public void SaveRoom(Rooms room)
        {
            ctx.Rooms.Add(room);
            ctx.SaveChanges();
        }

        public void SaveLocation(Locations location)
        {
            ctx.Locations.Add(location);
            ctx.SaveChanges();
        }

        /*public void SaveRestaurant(Activities restaurantActivitty)
        {
            Restaurants restaurant = restaurantActivitty.Restaurants.Single();
            for (DateTime start =restaurant.lunchStart; start<= restaurant.lunchEnd; start = start.AddMinutes(30))
            {
                Activities activity = new Activities();
                activity.typeActivity = 3;
                activity.highlight = restaurantActivitty.highlight;
                activity.price = restaurantActivitty.price;
                activity.startTime = start;
                restaurant.Activities.Add(activity);
            }
            ctx.Restaurants.Add(restaurant);
            ctx.SaveChanges();
        }*/

        public void SaveRestaurant(RestaurantsFormModel restaurantFrom)
        {
            Restaurants restaurant = new Restaurants();
            restaurant.descriptionEN = restaurantFrom.descriptionEN;
            restaurant.descriptionNL = restaurantFrom.descriptionNL;
            restaurant.dinnerEnd = restaurantFrom.dinnerEnd;
            restaurant.dinnerStart = restaurantFrom.dinnerStart;
            restaurant.locationId = restaurantFrom.locationId;
            restaurant.lunchEnd = restaurantFrom.lunchEnd;
            restaurant.lunchStart = restaurantFrom.lunchStart;
            restaurant.restaurantLogo = restaurantFrom.restaurantLogo;
            for (DateTime start = restaurant.lunchStart; start <= restaurant.lunchEnd; start = start.AddMinutes(30))
            {
                Activities activity = new Activities();
                activity.typeActivity = 3;
                activity.highlight = restaurantFrom.Highlight;
                activity.price = restaurantFrom.Price;
                activity.startTime = start;
                restaurant.Activities.Add(activity);
            }
            for (DateTime startDinner = restaurant.dinnerEnd; startDinner <= restaurant.dinnerEnd; startDinner = startDinner.AddMinutes(30))
            {
                Activities activity = new Activities();
                activity.typeActivity = 3;
                activity.highlight = restaurantFrom.Highlight;
                activity.price = restaurantFrom.Price;
                activity.startTime = startDinner;
                restaurant.Activities.Add(activity);
            }
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
            ctx.Entry(film).State = EntityState.Modified;
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
