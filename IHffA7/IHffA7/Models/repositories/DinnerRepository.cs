using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IHffA7.Models.repositories
{
    public class DinnerRepository : IDinnerRepository
    {
        //private IhffA7Context ctx = new IhffA7Context();
        private WhatsUp1617S_martinstinsGenerated ctx = new WhatsUp1617S_martinstinsGenerated();

        public int ConvertToActivityId(RestaurantOverviewModel rom)
        {
            //rom.Id
            //rom.Datum

            var restaurant = ctx.Restaurants.Find(rom.Id);
            var activityId = restaurant.Activities.First(a => a.startTime == rom.Datum).id;

            return activityId;
        }

        public IEnumerable<RestaurantOverviewModel> GetAllRestaurants()
        {
            IEnumerable<RestaurantOverviewModel> restaurantItems =
            (
                from r in ctx.Restaurants
                join l in ctx.Locations
                    on r.locationId equals l.id
                select new RestaurantOverviewModel
                {
                    Id = r.id,
                    Name = l.name,
                    DescriptionNL = r.descriptionNL,
                    Street = l.street,
                    StreetNumber = l.streetNumber,
                    ZipCode = l.zipCode,
                    Town = l.town,
                    LunchStart = r.lunchStart,
                    LunchEnd = r.lunchEnd,
                    DinnerStart = r.dinnerStart,
                    DinnerEnd = r.dinnerEnd,
                    RestaurantLogo = r.restaurantLogo,
                    Datum = default(DateTime),
                    Tijd = default(TimeSpan),
                    AantalPersonen = 0
                }
            );

            return restaurantItems;
        }
    }
}