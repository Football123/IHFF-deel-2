using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace IHffA7.Models
{
    class Filmview
    {
        int EventId { get; set; }
        int TypeId { get; set; }
        string Omschrijving { get; set; }
        decimal Prijs { get; set; }
        DateTime BeginDatumTijd { get; set; }
        DateTime EindDatumTijd { get; set; }
        int MaxAantal { get; set; }
        int FilmId { get; set; }
        int Rang { get; set; }
        decimal RangPrijs { get; set; }

        public Filmview(int eventId, int typeId, string omschrijving, decimal prijs, DateTime beginDatumTijd, DateTime eindDatumTijd
            , int maxAantal, int filmId, int rang, decimal rangPrijs)
        {
            EventId = eventId;
            TypeId = typeId;
            Omschrijving = omschrijving;
            Prijs = prijs;
            BeginDatumTijd = beginDatumTijd;
            EindDatumTijd = eindDatumTijd;
            MaxAantal = maxAantal;
            FilmId = filmId;
            Rang = rang;
            RangPrijs = rangPrijs;
        }

         public void AddToWishlist(int eventId, int typeId, int aantPersonen)
        {

        }
    }
}
