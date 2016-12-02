using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHffA7.Models.dbModels
{
    public class FilmScreenings
    {
        public int Id { get; set; }
        public int ActivityId { get; set; }
        public int FilmId { get; set; }
        public int MaxAvailableSeats { get; set; }

        public FilmScreenings()
        {
                
        }
        public FilmScreenings(int id, int activityId, int filmId, int maxAvailableSeats)
        {
            this.Id = id;
            this.ActivityId = activityId;
            this.FilmId = filmId;
            this.MaxAvailableSeats = maxAvailableSeats;
        }
    }
}
