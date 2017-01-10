using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace IHffA7.Models.repositories
{
    public class HomeRepository : IHomeRepository
    {
        private WhatsUp1617S_martinstinsGenerated ctx = new WhatsUp1617S_martinstinsGenerated();

        public IEnumerable<FilmsTodayModel> GetTodaysMovies()
        {
            var today = DateTime.Today.Date;

            var q = ctx.Activities.Select(t => t.startTime.Date);

            IEnumerable<FilmsTodayModel> filmTodayItems =
            (
                from f in ctx.Films
                join fs in ctx.Filmscreenings
                    on f.id equals fs.filmId
                join r in ctx.Rooms
                    on fs.roomId equals r.id
                join a in ctx.Activities
                    on fs.activityId equals a.id
                    where System.Data.Entity.DbFunctions.TruncateTime(a.startTime) == today
                join l in ctx.Locations
                    on r.locationId equals l.id
                select new FilmsTodayModel
                { 
                    filmLogo = f.filmLogo,
                    beginTime = a.startTime, 
                    endTime = a.endTime,
                    locationName = l.name,
                    roomName = r.name,
                    availableSeats = fs.availableSeats,
                    title = f.title,
                    price = a.price
                }
            );

            return filmTodayItems;
        }
    }
}