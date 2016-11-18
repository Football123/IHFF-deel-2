using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHffA7.Models.dbModels
{
    public class Wishlist
    {
        public int Id { get; set; }
        //kan probleem van db naar app opleveren
        public decimal TotaalPrijs { get; set; }
        //kan probleem van db naar app opleveren (BIT naar bool)
        public bool Betaald { get; set; }

        public Wishlist(int id, decimal totaalPrijs, bool betaald)
        {
            this.Id = id;
            this.TotaalPrijs = totaalPrijs;
            this.Betaald = betaald;
        }
    }
}
