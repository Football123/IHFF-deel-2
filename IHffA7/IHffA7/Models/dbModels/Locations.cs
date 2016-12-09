using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHffA7.Models.dbModels
{
    public class Locations
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string Town { get; set; }
        public int StreetNumber { get; set; }

        public Locations(int id, string name, string street, string zipCode, string town, int streetNumber)
        {
            this.Id = id;
            this.Name = name;
            this.Street = street;
            this.ZipCode = zipCode;
            this.Town = town;
            this.StreetNumber = streetNumber;
        }

        public Locations()
        {
        }
    }
}
