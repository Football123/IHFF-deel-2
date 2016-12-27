using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IHffA7.Models.repositories;

namespace IHffA7.Models
{
    public class WishlistItemFilm
    {
         public IList<WishlistItemFilm> WishlistFilmList { get; set; }
        public string Title { get; set; }
        public decimal TotalPrice { get; set; }
        public int AvailableSeats { get; set; }
        public string LocationName { get; set; }
        public string RoomName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public WishlistItemFilm()
        {
            WishlistFilmList = new List<WishlistItemFilm>();
        }
        public WishlistItemFilm(WishlistItems wishitem, Activities activiteit, Filmscreenings screening, Films film, Locations location, Rooms room, decimal totalprice)
        {
            Title = film.title;
            TotalPrice = totalprice;
            AvailableSeats = screening.availableSeats;
            LocationName = location.name;
            RoomName = room.name;
            StartTime = activiteit.startTime;
            EndTime = activiteit.endTime;
        }

        public void Add(WishlistItemFilm film)
        {
            WishlistFilmList.Add(film);
        }
    }
}
