using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHffA7.Models.repositories
{
    interface IDinnerRepository
    {
        IEnumerable<Dinner> GetRestaurants();
        IEnumerable<Dinner> GetRestaurants(string dag);
        Filmview GetRestaurant(Dinner dinner);
        void AddRestaurant(Dinner dinner);
        void RemoveRestaurant(Dinner dinner);
    }
}
