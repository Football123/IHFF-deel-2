using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHffA7.Models.dbModels
{
    public class Wishlists
    {
        public int Id { get; set; }
        //kan probleem van db naar app opleveren
        public decimal TotalPrice { get; set; }
        //kan probleem van db naar app opleveren (BIT naar bool)
        public bool Paid { get; set; }

        public Wishlists(int id, decimal totalPrice, bool paid)
        {
            this.Id = id;
            this.TotalPrice = totalPrice;
            this.Paid = paid;
        }
        public Wishlists()
        {

        }
    }
}
