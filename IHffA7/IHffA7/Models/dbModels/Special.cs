using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHffA7.Models.dbModels
{
    public class Special
    {
        public int Id { get; set; }
        public string ActiviteitId { get; set; }
        public string Titel { get; set; }
        public string BeschrijvingNL { get; set; }
        public string BeschrijvingEN { get; set; }
        public int MaxAantal { get; set; }

        public Special()
        {
                
        }

        public Special(int id, string activiteitId, string titel, string beschrijvingNL, string beschrijvingEN,
            int maxaantal)
        {
            this.Id = id;
            this.ActiviteitId = activiteitId;
            this.Titel = titel;
            this.BeschrijvingNL = beschrijvingNL;
            this.BeschrijvingEN = beschrijvingEN;
            this.MaxAantal = maxaantal;
        }
    }
}
