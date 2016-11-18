using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHffA7.Models.dbModels
{
    public class Film
    {
        public int Id { get; set; }
        public string Titel { get; set; }
        public string Regiseur { get; set; }
        public string BeschrijvingNL { get; set; }
        public string BeschrijvingEN { get; set; }
        public DateTime Jaar { get; set; }

        public Film(int id, string titel, string regiseur, string beschrijvingNL, string beschrijvingEN, DateTime jaar)
        {
            this.Id = id;
            this.Titel = titel;
            this.Regiseur = regiseur;
            this.BeschrijvingNL = beschrijvingNL;
            this.BeschrijvingEN = beschrijvingEN;
            this.Jaar = jaar;
        }
    }
}
