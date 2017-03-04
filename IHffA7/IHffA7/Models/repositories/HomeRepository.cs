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

        public int ConvertToFilmId(FilmsTodayModel ftm)
        {
            var convertedId =
                (from entry in ctx.Filmscreenings where (entry.activityId == ftm.activityId) select entry.filmId).FirstOrDefault();

            return convertedId;
        }


        public IEnumerable<HighlightFilmsModel> GetHighlightMovies()
        {
            var today = DateTime.Today.Date;

            var checkFutureHighlights =
                (from entry in ctx.Activities where ((System.Data.Entity.DbFunctions.TruncateTime(entry.startTime) >= today) && (entry.highlight == true)) select entry).FirstOrDefault();

            if (checkFutureHighlights != null)
            {
                IEnumerable<HighlightFilmsModel> highlightFilmItems =
                (
                    from f in ctx.Films
                    join fs in ctx.Filmscreenings
                        on f.id equals fs.filmId
                    join r in ctx.Rooms
                        on fs.roomId equals r.id
                    join a in ctx.Activities
                        on fs.activityId equals a.id
                    where ((System.Data.Entity.DbFunctions.TruncateTime(a.startTime) >= today) && (a.highlight == true))
                    join l in ctx.Locations
                        on r.locationId equals l.id
                    select new HighlightFilmsModel
                    {
                        activityId = fs.activityId,
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

                return highlightFilmItems;
            }
            else
            {
                IEnumerable<HighlightFilmsModel> highlightFilmItems = null;
                return highlightFilmItems;
            }
        }


        public IEnumerable<FilmsTodayModel> GetTodaysMovies()
        {
            var today = DateTime.Today.Date;
            
            var checkToday =
                (from entry in ctx.Activities where (System.Data.Entity.DbFunctions.TruncateTime(entry.startTime) == today) select entry).FirstOrDefault();

            if (checkToday != null)
            {
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
                        activityId = fs.activityId,
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
            else
            {
                IEnumerable<FilmsTodayModel> filmTodayItems = null;
                return filmTodayItems;
            }
        }
    }
}