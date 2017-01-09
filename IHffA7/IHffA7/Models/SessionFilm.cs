using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IHffA7.Models;
using IHffA7.Models.repositories;

namespace IHffA7.Models
{
    public class SessionFilm
    {
        public int NumberOfpersones;
        public int ActivityId;
        public DateTime? StartTime { get; set; }

        public SessionFilm(int numberOfpersones, int activityId, DateTime? startTime)
        {
            this.NumberOfpersones = numberOfpersones;
            this.ActivityId = activityId;
            StartTime = startTime;
        }
    }
}
