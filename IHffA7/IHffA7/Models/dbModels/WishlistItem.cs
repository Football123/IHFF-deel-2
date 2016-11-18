using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHffA7.Models.dbModels
{
    public class WishlistItem
    {
        public int Id { get; set; }
        public int WishlistId { get; set; }
        public int ActiviteitId { get; set; }
        public int AantalPersonen { get; set; }

        public WishlistItem(int id, int wishlistId, int activiteitId, int aantalpersonen)
        {
            this.Id = id;
            this.WishlistId = wishlistId;
            this.ActiviteitId = activiteitId;
            this.AantalPersonen = aantalpersonen;
        }

    }
}
