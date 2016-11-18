using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHffA7.Models.dbModels
{
    public class Locatie
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Straat { get; set; }
        public string Postcode { get; set; }
        public string Plaats { get; set; }
        public string Gebouw { get; set; }

        public Locatie(int id, string naam, string straat, string postcode, string plaats, string gebouw)
        {
            this.Id = id;
            this.Naam = naam;
            this.Straat = straat;
            this.Postcode = postcode;
            this.Plaats = plaats;
            this.Gebouw = gebouw;
        }

        public Locatie()
        {
        }
    }
}
