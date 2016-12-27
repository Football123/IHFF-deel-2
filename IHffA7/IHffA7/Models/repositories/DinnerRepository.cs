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
        public IEnumerable<RestaurantOverviewModel> GetAllRestaurants()
        {
            IList<RestaurantOverviewModel> restaurantItems = new List<RestaurantOverviewModel>() ;
            foreach( var res in ctx.Restaurants)
            {
                RestaurantOverviewModel r = new RestaurantOverviewModel();
                r.DescriptionNL = res.descriptionNL;
                r.DinnerEnd = r.DinnerEnd;
                r.DinnerStart = res.dinnerStart;
                r.Id = res.id;
                r.LunchStart = res.lunchStart;
                r.LunchEnd = res.lunchEnd;
                r.Name = res.Locations.name;
                r.RestaurantLogo = res.restaurantLogo;
                var restLocation = res.Locations;
                r.Street = restLocation.street;
                r.StreetNumber = restLocation.streetNumber;
                r.Town = restLocation.town;
                r.ZipCode = restLocation.zipCode;
                restaurantItems.Add(r);
            }
            /*(
                from r in ctx.Restaurant
                join a in ctx.Activity
                on r.Id equals a.Id
                join l in ctx.Location
                on a.Id equals l.Id
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
            )*/;

            return restaurantItems;
        }
    }
}