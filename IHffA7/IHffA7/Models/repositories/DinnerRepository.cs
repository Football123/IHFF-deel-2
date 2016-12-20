using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IHffA7.Models.repositories
{
    public class DinnerRepository : IDinnerRepository
    {
        private IhffA7Context ctx = new IhffA7Context();

        public IEnumerable<RestaurantOverviewModel> GetAllRestaurants()
        {
            IEnumerable<RestaurantOverviewModel> restaurantItems =
            (
                from r in ctx.Restaurant
                join a in ctx.Activity
                on r.Id equals a.Id
                join l in ctx.Location
                on a.LocationId equals l.Id
                select new RestaurantOverviewModel
                {
                    Id = r.Id,
                    Name = l.Name,
                    DescriptionNL = r.DescriptionNL,
                    Street = l.Street,
                    StreetNumber = l.StreetNumber,
                    ZipCode = l.ZipCode,
                    Town = l.Town,
                    LunchStart = r.LunchStart,
                    LunchEnd = r.LunchEnd,
                    DinnerStart = r.DinnerStart,
                    DinnerEnd = r.DinnerEnd,
                    RestaurantLogo = r.RestaurantLogo
                }
            );

            return restaurantItems;
        }
    }
}