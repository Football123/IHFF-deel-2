using System.Collections.Generic;

namespace IHffA7.Models.repositories
{
    interface IDinnerRepository
    {
        IEnumerable<RestaurantOverviewModel> GetAllRestaurants();
        int ConvertToActivityId(RestaurantOverviewModel rom);

    }
}
