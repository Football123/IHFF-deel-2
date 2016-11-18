using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHffA7.Models.dbModels
{
    public class Filmvoorstelling
    {
        public int Id { get; set; }
        public int ACtiviteitId { get; set; }
        public int FilmId { get; set; }
        public int MaxAantal { get; set; }
        public int LocatieId { get; set; }


    }
}
