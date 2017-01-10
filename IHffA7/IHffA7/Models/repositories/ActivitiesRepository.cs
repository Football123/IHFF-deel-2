using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace IHffA7.Models.repositories
{
    public class ActivitiesRepository
    {
        WhatsUp1617S_martinstinsGenerated ctx = new WhatsUp1617S_martinstinsGenerated(); //test offline db

        public void saveFilm(Films film)
        {
            //test
            ctx.Films.Add(film);
            ctx.SaveChanges();
        }
    }
}
