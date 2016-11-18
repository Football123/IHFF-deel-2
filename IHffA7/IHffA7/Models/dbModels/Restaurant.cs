using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHffA7.Models.dbModels
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string ActiviteitId { get; set; }
        public string BeschrijvingNL { get; set; }
        public string BeschrijvingEN { get; set; }
        public string LunchStart { get; set; }
        public string LunchEind { get; set; }
        public string DinnerStart { get; set; }
        public string DinnerEind { get; set; }
        //
        public Restaurant(int id, string activiteitId, string beschrijvingNL, string beschrijvingEN,
            string lunchStart, string lunchEind, string dinnerStart, string dinnerEind)
        {
            this.Id = id;
            this.ActiviteitId = activiteitId;
            this.BeschrijvingNL = beschrijvingNL;
            this.BeschrijvingEN = beschrijvingEN;
            this.LunchStart = lunchStart;
            this.LunchEind=lunchEind;
            this.DinnerStart = dinnerStart;
            this.DinnerEind = dinnerEind;

        }

        public Restaurant()
        {

        }
    }
}
