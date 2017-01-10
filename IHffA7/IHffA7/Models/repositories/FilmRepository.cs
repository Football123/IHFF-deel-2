using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHffA7.Models.repositories
{
    class FilmRepository
    {
        private WhatsUp1617S_martinstinsGenerated ctx = new WhatsUp1617S_martinstinsGenerated();
        public Films GetFilm(int filmId)
        {
            var i = 
                ctx.Films.SingleOrDefault (c => (c.id == filmId));
            return i;
            
        }
    }
}
