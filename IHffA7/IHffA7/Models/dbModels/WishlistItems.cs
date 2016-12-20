using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHffA7.Models.dbModels
{
    public class WishlistItems
    {
        public int Id { get; set; }
        public int WishlistId { get; set; }
        public int ActivityId { get; set; }
        public int NumberOfPersons { get; set; }

        public WishlistItems(int id, int wishlistId, int activityId, int numberOfPersons)
        {
            this.Id = id;
            this.WishlistId = wishlistId;
            this.ActivityId = activityId;
            this.NumberOfPersons = numberOfPersons;
        }
        public WishlistItems()
        {

        }
    }
}
