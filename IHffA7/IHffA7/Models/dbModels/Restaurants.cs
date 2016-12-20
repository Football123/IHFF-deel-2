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
        public string LunchStart { get; set; }
        public string LunchEnd { get; set; }
        public string DinnerStart { get; set; }
        public string DinnerEnd { get; set; }
        //
        public Restaurants(int id, string activityId, string descriptionNL, string descriptionEN,
            string lunchStart, string lunchEnd, string dinnerStart, string dinnerEnd)
        {
            this.Id = id;
            this.ActivityId = activityId;
            this.DescriptionNL = descriptionNL;
            this.DescriptionEN = descriptionEN;
            this.LunchStart = lunchStart;
            this.LunchEnd = lunchEnd;
            this.DinnerStart = dinnerStart;
            this.DinnerEnd = dinnerEnd;

        }

        public Restaurants()
        {

        }
    }
}
