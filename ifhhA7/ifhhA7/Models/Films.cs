using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ifhhA7.Models
{
    class Films
    {
        public int EventId { get; set; }
        public string Omschrijving { get; set; }
        public int Prijs { get; set; }
        public DateTime BeginDatum { get; set; }
        public DateTime EindDatum { get; set; }
        public int MaxAantal { get; set; }
        public int Rang { get; set; }
        public decimal RangPrijs { get; set; }
    }
}
