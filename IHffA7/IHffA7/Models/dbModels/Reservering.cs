using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHffA7.Models.dbModels
{
    public class Reservering
    {
        public int Id { get; set; }
        public int WishlistId { get; set; }
        public string Naam { get; set; }
        public string Email { get; set; }
        public string Betaalmethode { get; set; }

        public Reservering(int id, int wishlistId, string naam, string email, string betaalmethode)
        {
            this.Id = id;
            this.WishlistId = wishlistId;
            this.Naam = naam;
            this.Email = email;
            this.Betaalmethode = betaalmethode;
        }
    }

}
