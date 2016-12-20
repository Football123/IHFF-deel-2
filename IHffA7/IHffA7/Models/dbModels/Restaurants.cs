using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHffA7.Models.dbModels
{
    public class Restaurants
    {
        public int Id { get; set; }
        public string ActivityId { get; set; }
        public string DescriptionNL { get; set; }
        public string DescriptionEN { get; set; }
        public DateTime LunchStart { get; set; }
        public DateTime LunchEnd { get; set; }
        public DateTime DinnerStart { get; set; }
        public DateTime DinnerEnd { get; set; }
        public int MaxAvailableSeats { get; set; }
        public string RestaurantLogo { get; set; }
        //
        public Restaurants(int id, string activityId, string descriptionNL, string descriptionEN,
            DateTime lunchStart, DateTime lunchEnd, DateTime dinnerStart, DateTime dinnerEnd,
            string restaurantLogo)
        {
            this.Id = id;
            this.ActivityId = activityId;
            this.DescriptionNL = descriptionNL;
            this.DescriptionEN = descriptionEN;
            this.LunchStart = lunchStart;
            this.LunchEnd = lunchEnd;
            this.DinnerStart = dinnerStart;
            this.DinnerEnd = dinnerEnd;
            this.RestaurantLogo = restaurantLogo;
        }

        public Restaurants()
        {

        }
    }
}
