using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHffA7.Models
{
    public class WishlistViewModel
    {
        public Activities Activity { get; set; }
        public Reservations Reservation { get; set; }
        public int NumberOfPersons { get; set; }
        public decimal totalprice { get; set; }
        public int TypeActivity { get; set; }
        public WishlistViewModel(Activities activity, int numerOferPersons)
        {
            Activity = activity;
            NumberOfPersons = numerOferPersons;
            totalprice = activity.price * numerOferPersons;
            TypeActivity = activity.typeActivity;
        }
        public WishlistViewModel()
        {

        }
    }

}
