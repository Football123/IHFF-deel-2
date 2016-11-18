using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHffA7.Models
{
    class Dinner
    {
        int EventId { get; set; }
        int TypeId { get; set; }
        string Omschrijving { get; set; }
        decimal Prijs { get; set; }
        DateTime BeginDatumTijd { get; set; }
        DateTime EindDatumTijd { get; set; }
        int MaxAantal { get; set; }
        int DinnerId { get; set; }
        bool Vervoer { get; set; }

        public Dinner(int eventId, int typeId, string omschrijving, decimal prijs, DateTime beginDatumTijd, DateTime eindDatumTijd
            , int maxAantal, int dinnerId, bool vervoer)
        {
            EventId = eventId;
            TypeId = typeId;
            Omschrijving = omschrijving;
            Prijs = prijs;
            BeginDatumTijd = beginDatumTijd;
            EindDatumTijd = eindDatumTijd;
            MaxAantal = maxAantal;
            DinnerId = dinnerId;
            Vervoer = vervoer;
        }

        public void AddToWishlist(int eventId, int typeId, int aantPersonen)
        {

        }
    }
}
