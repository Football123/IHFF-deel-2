using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHffA7.Models.dbModels
{
    public class Reservations
    {
        public int Id { get; set; }
        public int WishlistId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PaymentMethod { get; set; }

        public Reservations(int id, int wishlistId, string name, string email, string paymentMethod)
        {
            this.Id = id;
            this.WishlistId = wishlistId;
            this.Name = name;
            this.Email = email;
            this.PaymentMethod = paymentMethod;
        }
    }

}
