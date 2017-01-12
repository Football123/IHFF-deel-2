using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IHffA7.Models
{
    public class FilmsTodayModel
    {
        // Films:
        public string filmLogo { get; set; }
        public string title { get; set; }
        // Activities:
        public int activityId { get; set; }
        public DateTime beginTime { get; set; }
        public DateTime? endTime { get; set; }
        public decimal price { get; set; }
        // Location:
        public string locationName { get; set; }
        // Rooms:
        public string roomName { get; set; }
        // Filmscreenings:
        public int availableSeats { get; set; }
    }
}