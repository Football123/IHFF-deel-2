using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using IHffA7.Models.dbModels;

namespace IHffA7.Models
{
    class IhffA7Context : DbContext
    {
        public IhffA7Context() : base("iHFF1617S_A7")
        {
            
        }
        public DbSet<Activiteit> Activiteit { get; set; }
        public DbSet<Film> Film { get; set; }
        public DbSet<Filmvoorstelling> Filmvoorstelling { get; set; }
        public DbSet<Reservering> Reservering { get; set; }
        public DbSet<Locatie> Locatie { get; set; }
        public DbSet<Restaurant> Restaurant { get; set; }
        public DbSet<Special> Special { get; set; }
        public DbSet<Wishlist> Wishlist { get; set; }
        public DbSet<WishlistItem> WishlistItem { get; set; }


    }
}
