using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using IHffA7.Models.dbModels;

namespace IHffA7.Models
{
    public class IhffA7Context : DbContext
    {
        public IhffA7Context() : base("iHFF1617S_A7")
        {
           // Database.SetInitializer<IhffA7Context>(null);
        }
        public DbSet<Activities> Activity { get; set; }
        public DbSet<Films> Film { get; set; }
        public DbSet<FilmScreenings> FilmScreening { get; set; }
        public DbSet<Reservations> Reservation { get; set; }
        public DbSet<Locations> Location { get; set; }
        public DbSet<Restaurants> Restaurant { get; set; }
        //public DbSet<Specials> Special { get; set; }
        public DbSet<Wishlists> Wishlist { get; set; }
        public DbSet<WishlistItems> WishlistItem { get; set; }
        //for test purpose only!
        public DbSet<Testtabel> Teststabels { get; set; }


    }
}
