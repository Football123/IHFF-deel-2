using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHffA7.Models
{
    public class SessionRestaurant
    {
        public int NumberOfpersones;
        public DateTime Start;
        public int LocationId;
        public int RestaurantId;

        public SessionRestaurant(int numberOfpersones, DateTime startTime, int locationId, int restaurantId)
        {
            this.NumberOfpersones = numberOfpersones;
            this.Start = startTime;
            this.LocationId = locationId;
            this.RestaurantId = restaurantId;
        }
    }
}
