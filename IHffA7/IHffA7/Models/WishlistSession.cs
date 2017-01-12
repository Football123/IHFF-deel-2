using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHffA7.Models
{
    public class WishlistSession
    {
        public int NumberOfpersones { get; set; }
        public int ActivityId { get; set; }
        public WishlistSession(int activityId, int numberOferPersons)
        {
            NumberOfpersones = numberOferPersons;
            ActivityId = activityId;
        }
        public WishlistSession()
        {

        }
    }

}
